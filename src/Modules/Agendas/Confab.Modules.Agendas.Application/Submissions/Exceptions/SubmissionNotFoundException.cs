using System;
using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Agendas.Application.Submissions.Exceptions
{
    public class SubmissionNotFoundException : ConfabException
    {
        public Guid SubmissionId { get; }

        public SubmissionNotFoundException(Guid submissionId) 
            : base($"Submission with ID: '{submissionId}' was not found.")
            => SubmissionId = submissionId;
    }
}