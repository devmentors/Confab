using System;
using Confab.Shared.Abstractions.Events;

namespace Confab.Modules.Agendas.Application.Submissions.Events.External
{
    public record SpeakerCreated(Guid Id, string FullName) : IEvent;
}