using System;
using Confab.Shared.Abstractions.Events;

namespace Confab.Modules.Agendas.Application.Submissions.Events
{
    public record SubmissionApproved(Guid Id) : IEvent;
}