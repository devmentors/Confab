using System;
using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Domain.Agendas.Entities;
using Confab.Modules.Agendas.Domain.Agendas.Exceptions;
using Confab.Modules.Agendas.Domain.Agendas.Repositories;
using Confab.Shared.Abstractions.Kernel.Types;

namespace Confab.Modules.Agendas.Domain.Agendas.Services
{
    public sealed class AgendaTracksDomainService : IAgendaTracksDomainService
    {
        private readonly IAgendaTracksRepository _agendaTracksRepository;
        private readonly IAgendaItemsRepository _agendaItemsRepository;
        
        public AgendaTracksDomainService(IAgendaTracksRepository repository, IAgendaItemsRepository agendaItemsRepository)
        {
            _agendaTracksRepository = repository;
            _agendaItemsRepository = agendaItemsRepository;
        }

        public async Task AssignAgendaItemAsync(AgendaTrack agendaTrack, EntityId agendaSlotId, EntityId agendaItemId)
        {
            var agendaTracks = await _agendaTracksRepository.BrowseAsync(agendaTrack.ConferenceId);

            var slotToAssign = agendaTrack.Slots.OfType<RegularAgendaSlot>().SingleOrDefault(s => s.Id == agendaSlotId);

            if (slotToAssign is null)
            {
                throw new AgendaSlotNotFoundException(agendaSlotId);
            }

            var agendaItem = await _agendaItemsRepository.GetAsync((Guid) agendaItemId);

            if (agendaItem is null)
            {
                throw new AgendaItemNotFoundException((Guid) agendaItemId);
            }

            var speakerIds = agendaItem.Speakers.Select(s => new SpeakerId(s.Id));
            var speakersItems = await _agendaItemsRepository.BrowseAsync(speakerIds);
            var speakersItemIds = speakersItems.Select(si => (Guid) si.Id).ToList();

            var hasCollidingSpeakerSlots = agendaTracks
                .SelectMany(at => at.Slots)
                .OfType<RegularAgendaSlot>()
                .Any(s => speakersItemIds.Contains(s.Id) && s.From < slotToAssign.To && slotToAssign.From > s.To);

            if (hasCollidingSpeakerSlots)
            {
                throw new CollidingSpeakerAgendaSlotsException(agendaSlotId, agendaItemId);
            }

            agendaTrack.ChangeSlotAgendaItem(agendaSlotId, agendaItem);
        }
    }
}