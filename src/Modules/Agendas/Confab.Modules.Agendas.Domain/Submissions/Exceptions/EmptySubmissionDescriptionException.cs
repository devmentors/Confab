using System;
using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Agendas.Domain.Submissions.Exceptions
{
    public class EmptySubmissionDescriptionException : ConfabException
    {
        public Guid SubmissionId { get; }

        public EmptySubmissionDescriptionException(Guid submissionId) 
            : base($"Submission with ID: '{submissionId}' defines empty description.")
            => SubmissionId = submissionId;
    }
}