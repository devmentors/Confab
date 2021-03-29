using System;
using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Agendas.Domain.Agendas.Exceptions
{
    public class EmptyAgendaItemTagsException : ConfabException
    {
        public Guid AgendaItemId { get; }
        
        public EmptyAgendaItemTagsException(Guid agendaItemId) 
            : base($"Agenda Item with id: '{agendaItemId}' defines empty tags.")
            => AgendaItemId = agendaItemId;
    }
}