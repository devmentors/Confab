using System;
using System.Collections.Generic;

namespace Confab.Modules.Agendas.Application.Submissions.DTO
{
    public class SubmissionDto
    {
        public Guid Id { get; set; }
        public Guid ConferenceId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public string Status { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public IEnumerable<SpeakerDto> Speakers { get; set; }
    }
}