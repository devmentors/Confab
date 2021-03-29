using System;
using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Agendas.Domain.Agendas.Exceptions
{
    public class ConflictingAgendaSlotsException : ConfabException
    {
        public ConflictingAgendaSlotsException(DateTime from, DateTime to) 
            : base($"There is slot conflicting with date range: {from} | {to}.")
        {
        }
    }
}