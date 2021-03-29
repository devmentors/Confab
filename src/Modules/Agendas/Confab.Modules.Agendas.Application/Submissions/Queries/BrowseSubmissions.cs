using System;
using System.Collections.Generic;
using Confab.Modules.Agendas.Application.Submissions.DTO;
using Confab.Shared.Abstractions.Queries;

namespace Confab.Modules.Agendas.Application.Submissions.Queries
{
    public class BrowseSubmissions : IQuery<IEnumerable<SubmissionDto>>
    {
        public Guid? ConferenceId { get; set; }
        public Guid? SpeakerId { get; set; }
    }
}