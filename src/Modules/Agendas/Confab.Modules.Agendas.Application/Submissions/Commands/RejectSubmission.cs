using System;
using Confab.Shared.Abstractions.Commands;

namespace Confab.Modules.Agendas.Application.Submissions.Commands
{
    public record RejectSubmission(Guid Id) : ICommand;
}