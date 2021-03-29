using System;
using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Agendas.Domain.CallForPapers.Exceptions
{
    internal class InvalidCallForPapersDatesException : ConfabException
    {
        public DateTime From { get; }
        public DateTime To { get; }

        public InvalidCallForPapersDatesException(DateTime from, DateTime to)
            : base($"CFP has invalid dates, from: '{from:d}' > to: '{to:d}'.")
        {
            From = from;
            To = to;
        }
    }
}