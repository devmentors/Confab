using System;
using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Agendas.Domain.Agendas.Exceptions
{
    public class InvalidAgendaSlotTypeException : ConfabException
    {
        public InvalidAgendaSlotTypeException(Guid agendaSlotId) 
            : base($"Agenda slot with ID: '{agendaSlotId}' has type which does not allow to perform desired operation.")
        {
        }
    }
}