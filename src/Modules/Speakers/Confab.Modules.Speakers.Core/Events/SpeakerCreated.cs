using System;
using Confab.Shared.Abstractions.Events;

namespace Confab.Modules.Speakers.Core.Events
{
    public record SpeakerCreated(Guid Id, string FullName) : IEvent;
}