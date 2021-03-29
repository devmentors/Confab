using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Attendances.Application.Clients.Agendas;
using Confab.Modules.Attendances.Domain.Entities;
using Confab.Modules.Attendances.Domain.Exceptions;
using Confab.Modules.Attendances.Domain.Policies;
using Confab.Modules.Attendances.Domain.Repositories;
using Confab.Shared.Abstractions.Events;

namespace Confab.Modules.Attendances.Application.Events.External.Handlers
{
    internal sealed class AgendaItemAssignedToAgendaSlotHandler : IEventHandler<AgendaItemAssignedToAgendaSlot>
    {
        private readonly IAttendableEventsRepository _attendableEventsRepository;
        private readonly IAgendasApiClient _agendasApiClient;
        private readonly ISlotPolicyFactory _slotPolicyFactory;

        public AgendaItemAssignedToAgendaSlotHandler(IAttendableEventsRepository attendableEventsRepository,
            IAgendasApiClient agendasApiClient, ISlotPolicyFactory slotPolicyFactory)
        {
            _attendableEventsRepository = attendableEventsRepository;
            _agendasApiClient = agendasApiClient;
            _slotPolicyFactory = slotPolicyFactory;
        }
        
        public async Task HandleAsync(AgendaItemAssignedToAgendaSlot @event)
        {
            var attendableEvent = await _attendableEventsRepository.GetAsync(@event.AgendaItemId);
            if (attendableEvent is not null)
            {
                return;
            }

            var slot = await _agendasApiClient.GetRegularAgendaSlotAsync(@event.AgendaItemId);
            if (slot is null)
            {
                throw new AttendableEventNotFoundException(@event.AgendaItemId);
            }

            if (!slot.ParticipantsLimit.HasValue)
            {
                return;
            }
            
            attendableEvent = new AttendableEvent(@event.AgendaItemId, slot.AgendaItem.ConferenceId, slot.From, slot.To);
            var slotPolicy = _slotPolicyFactory.Get(slot.AgendaItem.Tags.ToArray());
            var slots = slotPolicy.Generate(slot.ParticipantsLimit.Value);
            attendableEvent.AddSlots(slots);
            await _attendableEventsRepository.AddAsync(attendableEvent);
        }
    }
}