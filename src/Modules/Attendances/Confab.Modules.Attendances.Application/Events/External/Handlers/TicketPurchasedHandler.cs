using System;
using System.Threading.Tasks;
using Confab.Modules.Attendances.Domain.Entities;
using Confab.Modules.Attendances.Domain.Repositories;
using Confab.Shared.Abstractions.Events;
using Microsoft.Extensions.Logging;

namespace Confab.Modules.Attendances.Application.Events.External.Handlers
{
    internal sealed class TicketPurchasedHandler : IEventHandler<TicketPurchased>
    {
        private readonly IParticipantsRepository _participantsRepository;
        private readonly ILogger<TicketPurchasedHandler> _logger;

        public TicketPurchasedHandler(IParticipantsRepository participantsRepository,
            ILogger<TicketPurchasedHandler> logger)
        {
            _participantsRepository = participantsRepository;
            _logger = logger;
        }
        
        public async Task HandleAsync(TicketPurchased @event)
        {
            var participant = await _participantsRepository.GetAsync(@event.ConferenceId, @event.UserId);
            if (participant is not null)
            {
                return;
            }

            participant = new Participant(Guid.NewGuid(), @event.ConferenceId, @event.UserId);
            await _participantsRepository.AddAsync(participant);
            _logger.LogInformation($"Added a participant with ID: '{participant.Id}' " +
                                   $"for conference: '{participant.ConferenceId}', user: '{participant.UserId}'.");
        }
    }
}