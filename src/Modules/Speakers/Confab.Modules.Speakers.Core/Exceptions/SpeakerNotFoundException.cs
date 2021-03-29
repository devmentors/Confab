using System;
using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Speakers.Core.Exceptions
{
    public class SpeakerNotFoundException : ConfabException
    {
        public Guid Id { get; }

        public SpeakerNotFoundException(Guid id) : base($"Speaker with id '{id} was not found.'")
            => Id = id;
    }
}