using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Agendas.Domain.Agendas.Exceptions
{
    public class EmptyAgendaSlotPlaceholderException : ConfabException
    {
        public EmptyAgendaSlotPlaceholderException() 
            : base($"Agenda slot defined empty placeholder'")
        {
        }
    }
}