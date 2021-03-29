using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Agendas.Application.Agendas.Exceptions
{
    public class AgendaSlotTypeNotFoundException : ConfabException
    {
        public string Type { get; }

        public AgendaSlotTypeNotFoundException(string type) : base($"Agenda slot type: '{type}' was not found.")
        {
            Type = type;
        }
    }
}