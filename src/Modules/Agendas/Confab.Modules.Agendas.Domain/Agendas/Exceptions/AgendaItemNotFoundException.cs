using System;
using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Agendas.Domain.Agendas.Exceptions
{
    public class AgendaItemNotFoundException : ConfabException
    {
        public AgendaItemNotFoundException(Guid agendaItemId) 
            : base($"Agenda item with ID: '{agendaItemId}' was not found.")
        {
        }
    }
}