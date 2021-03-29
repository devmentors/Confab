using System;
using Confab.Shared.Abstractions.Events;

namespace Confab.Modules.Saga.Messages
{
    public record SpeakerCreated(Guid Id, string FullName) : IEvent;
}