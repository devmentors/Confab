using System;
using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Agendas.Domain.Agendas.Exceptions
{
    public class NegativeParticipantsLimitException : ConfabException
    {
        public NegativeParticipantsLimitException(Guid agendaSlotId) 
            : base($"Regular slot with ID: '{agendaSlotId}' defines negative participants limit.")
        {
        }
    }
}