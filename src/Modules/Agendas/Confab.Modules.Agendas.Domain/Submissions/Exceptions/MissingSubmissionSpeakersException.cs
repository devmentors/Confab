using System;
using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Agendas.Domain.Submissions.Exceptions
{
    public class MissingSubmissionSpeakersException : ConfabException
    {
        public Guid SubmissionId { get; }

        public MissingSubmissionSpeakersException(Guid submissionId) 
            : base($"Submission with ID: '{submissionId}' has missing speakers.")
            => SubmissionId = submissionId;
    }
}