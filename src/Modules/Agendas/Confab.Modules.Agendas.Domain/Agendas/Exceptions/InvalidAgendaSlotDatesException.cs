using System;
using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Agendas.Domain.Agendas.Exceptions
{
    public class InvalidAgendaSlotDatesException : ConfabException
    {
        public DateTime From { get; }
        public DateTime To { get; }

        public InvalidAgendaSlotDatesException(DateTime from, DateTime to)
            : base($"Agenda track has invalid dates, from: '{from:d}' > to: '{to:d}'.")
        {
            From = from;
            To = to;
        }
    }
}