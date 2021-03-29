using System;
using System.Collections.Generic;
using Confab.Shared.Abstractions.Commands;

namespace Confab.Modules.Agendas.Application.Submissions.Commands
{
    public record CreateSubmission(Guid ConferenceId, string Title, string Description, int Level,
        IEnumerable<string> Tags, IEnumerable<Guid> SpeakerIds) : ICommand
    {
        public Guid Id { get; } = Guid.NewGuid();
    }
}