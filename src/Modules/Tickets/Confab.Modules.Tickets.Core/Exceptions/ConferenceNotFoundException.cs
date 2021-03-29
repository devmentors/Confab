using System;
using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Tickets.Core.Exceptions
{
    internal class ConferenceNotFoundException : ConfabException
    {
        public Guid Id { get; }

        public ConferenceNotFoundException(Guid id) : base($"Conference with ID: '{id}' was not found.")
        {
            Id = id;
        }
    }
}