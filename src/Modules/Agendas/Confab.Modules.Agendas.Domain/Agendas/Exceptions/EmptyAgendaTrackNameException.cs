using System;
using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Agendas.Domain.Agendas.Exceptions
{
    public class EmptyAgendaTrackNameException : ConfabException
    {
        public EmptyAgendaTrackNameException(Guid agendaTrackId) 
            : base($"Agenda track with ID: {agendaTrackId} defines empty name.")
        {
        }
    }
}