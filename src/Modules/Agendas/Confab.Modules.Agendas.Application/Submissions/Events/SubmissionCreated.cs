using System;
using Confab.Shared.Abstractions.Events;

namespace Confab.Modules.Agendas.Application.Submissions.Events
{
    public record SubmissionCreated(Guid Id) : IEvent;
}