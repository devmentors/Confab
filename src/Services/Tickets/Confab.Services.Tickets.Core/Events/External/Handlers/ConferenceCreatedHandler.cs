using System.Threading.Tasks;
using Confab.Services.Tickets.Core.Entities;
using Confab.Services.Tickets.Core.Repositories;
using Convey.CQRS.Events;
using Microsoft.Extensions.Logging;

namespace Confab.Services.Tickets.Core.Events.External.Handlers
{
    internal sealed class ConferenceCreatedHandler : IEventHandler<ConferenceCreated>
    {
        private readonly IConferenceRepository _conferenceRepository;
        private readonly ILogger<ConferenceCreatedHandler> _logger;

        public ConferenceCreatedHandler(IConferenceRepository conferenceRepository,
            ILogger<ConferenceCreatedHandler> logger)
        {
            _conferenceRepository = conferenceRepository;
            _logger = logger;
        }

        public async Task HandleAsync(ConferenceCreated @event)
        {
            var conference = new Conference
            {
                Id = @event.Id,
                Name = @event.Name,
                ParticipantsLimit = @event.ParticipantsLimit,
                From = @event.From,
                To = @event.To
            };

            await _conferenceRepository.AddAsync(conference);
            _logger.LogInformation($"Added a conference with ID: '{@event.Id}'.");
        }
    }
}