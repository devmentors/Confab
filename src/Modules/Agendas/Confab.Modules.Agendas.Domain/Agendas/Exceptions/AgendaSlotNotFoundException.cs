using System;
using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Agendas.Domain.Agendas.Exceptions
{
    public class AgendaSlotNotFoundException : ConfabException
    {
        public AgendaSlotNotFoundException(Guid agendaSlotId) 
            : base($"Agenda slot with ID: '{agendaSlotId}' was not found.")
        {
        }
    }
}