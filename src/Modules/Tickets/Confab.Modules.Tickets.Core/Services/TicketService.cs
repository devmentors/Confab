using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Tickets.Core.DTO;
using Confab.Modules.Tickets.Core.Entities;
using Confab.Modules.Tickets.Core.Events;
using Confab.Modules.Tickets.Core.Exceptions;
using Confab.Modules.Tickets.Core.Repositories;
using Confab.Shared.Abstractions.Messaging;
using Confab.Shared.Abstractions.Time;
using Microsoft.Extensions.Logging;

namespace Confab.Modules.Tickets.Core.Services
{
    internal class TicketService : ITicketService
    {
        private readonly IClock _clock;
        private readonly IConferenceRepository _conferenceRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly ITicketSaleRepository _ticketSaleRepository;
        private readonly ITicketGenerator _generator;
        private readonly IMessageBroker _messageBroker;
        private readonly ILogger<TicketService> _logger;

        public TicketService(IClock clock, IConferenceRepository conferenceRepository,
            ITicketRepository ticketRepository, ITicketSaleRepository ticketSaleRepository, ITicketGenerator generator,
            IMessageBroker messageBroker, ILogger<TicketService> logger)
        {
            _clock = clock;
            _conferenceRepository = conferenceRepository;
            _ticketRepository = ticketRepository;
            _ticketSaleRepository = ticketSaleRepository;
            _generator = generator;
            _messageBroker = messageBroker;
            _logger = logger;
        }
        
        public async Task PurchaseAsync(Guid conferenceId, Guid userId)
        {
            var conference = await _conferenceRepository.GetAsync(conferenceId);
            if (conference is null)
            {
                throw new ConferenceNotFoundException(conferenceId);
            }

            var ticket = await _ticketRepository.GetAsync(conferenceId, userId);
            if (ticket is not null)
            {
                throw new TicketAlreadyPurchasedException(conferenceId, userId);
            }

            var now = _clock.CurrentDate();
            var ticketSale = await _ticketSaleRepository.GetCurrentForConferenceAsync(conferenceId, now);
            if (ticketSale is null)
            {
                throw new TicketSaleUnavailableException(conferenceId);
            }
            
            if (ticketSale.Amount.HasValue)
            {
                await PurchaseAvailableAsync(ticketSale, userId, ticketSale.Price);
                return;
            }

            ticket = _generator.Generate(conferenceId, ticketSale.Id, ticketSale.Price);
            ticket.Purchase(userId, _clock.CurrentDate(), ticketSale.Price);
            await _ticketRepository.AddAsync(ticket);
            _logger.LogInformation($"Ticket with ID: '{ticket.Id}' was generated for the conference: " +
                                   $"'{conferenceId}' by user: '{userId}'.");
            await _messageBroker.PublishAsync(new TicketPurchased(ticket.Id, conferenceId, userId));
        }
        
        private async Task PurchaseAvailableAsync(TicketSale ticketSale, Guid userId, decimal? price)
        {
            var conferenceId = ticketSale.ConferenceId;
            var ticket = ticketSale.Tickets.Where(x => x.UserId is null).OrderBy(_ => Guid.NewGuid()).FirstOrDefault();
            if (ticket is null)
            {
                throw new TicketsUnavailableException(conferenceId);
            }

            ticket.Purchase(userId, _clock.CurrentDate(), price);
            await _ticketRepository.UpdateAsync(ticket);
            _logger.LogInformation($"Ticket with ID: '{ticket.Id}' was purchased for the conference: " +
                                   $"'{conferenceId}' by user: '{userId}'.");
            await _messageBroker.PublishAsync(new TicketPurchased(ticket.Id, conferenceId, userId));
        }

        public async Task<IEnumerable<TicketDto>> GetForUserAsync(Guid userId)
        {
            var tickets = await _ticketRepository.GetForUserAsync(userId);

            return tickets.Select(x => new TicketDto(x.Code, x.Price, x.PurchasedAt.Value,
                    new ConferenceDto(x.ConferenceId, x.Conference.Name)))
                .OrderBy(x => x.PurchasedAt);
        }
    }
}