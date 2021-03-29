using System;
using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Agendas.Application.CallForPapers.Exceptions
{
    internal class CallForPapersClosedException : ConfabException
    {
        public Guid ConferenceId { get; }

        public CallForPapersClosedException(Guid conferenceId)
            : base($"Conference with ID: '{conferenceId}' has closed CFP.")
            => ConferenceId = conferenceId;
    }
}