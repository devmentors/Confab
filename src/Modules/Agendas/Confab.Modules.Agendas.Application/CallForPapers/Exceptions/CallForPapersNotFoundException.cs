using System;
using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Agendas.Application.CallForPapers.Exceptions
{
    internal class CallForPapersNotFoundException : ConfabException
    {
        public Guid ConferenceId { get; }

        public CallForPapersNotFoundException(Guid conferenceId)
            : base($"Conference with ID: '{conferenceId}' has no CFP.")
            => ConferenceId = conferenceId;
    }
}