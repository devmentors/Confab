using System.Linq;
using Confab.Modules.Agendas.Application.Submissions.DTO;
using Confab.Modules.Agendas.Domain.Submissions.Entities;

namespace Confab.Modules.Agendas.Infrastructure.EF.Mappings
{
    internal static class SubmissionExtensions
    {
        public static SubmissionDto AsDto(this Submission submission)
            => new()
            {
                Id = submission.Id,
                ConferenceId = submission.ConferenceId,
                Title = submission.Title,
                Description = submission.Description,
                Level = submission.Level,
                Status = submission.Status,
                Tags = submission.Tags,
                Speakers = submission.Speakers.Select(s => new SpeakerDto
                {
                    Id = s.Id,
                    FullName = s.FullName
                })
            };
    }
}