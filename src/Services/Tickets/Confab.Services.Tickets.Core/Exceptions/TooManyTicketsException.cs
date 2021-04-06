using System;
using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Services.Tickets.Core.Exceptions
{
    public class TooManyTicketsException : ConfabException
    {
        public Guid ConferenceId { get; }

        public TooManyTicketsException(Guid conferenceId)
            : base("Too many tickets would be generated for the conference.")

        {
            ConferenceId = conferenceId;
        }
    }
}