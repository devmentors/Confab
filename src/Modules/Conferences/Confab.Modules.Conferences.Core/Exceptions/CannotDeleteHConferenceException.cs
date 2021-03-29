using System;
using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Conferences.Core.Exceptions
{
    internal class CannotDeleteHConferenceException : ConfabException
    {
        public Guid Id { get; }

        public CannotDeleteHConferenceException(Guid id) : base($"Conference with ID: '{id}' cannot be deleted.")
        {
            Id = id;
        }
    }
}