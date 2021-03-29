using System;
using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Agendas.Domain.Agendas.Exceptions
{
    public class CollidingSpeakerAgendaSlotsException : ConfabException
    {
        public CollidingSpeakerAgendaSlotsException(Guid agendaSlotId, Guid agendaItemId) 
            : base($"Cannot assign agenda item with ID: '{agendaItemId}' to slot with ID: '{agendaSlotId}'")
        {
        }
    }
}