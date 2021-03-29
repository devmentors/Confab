using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.Submissions.DTO;
using Confab.Modules.Agendas.Application.Submissions.Queries;
using Confab.Modules.Agendas.Domain.Submissions.Entities;
using Confab.Modules.Agendas.Infrastructure.EF.Mappings;
using Confab.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Agendas.Infrastructure.EF.Queries.Handlers
{
    internal sealed class BrowseSubmissionsHandler : IQueryHandler<BrowseSubmissions, IEnumerable<SubmissionDto>>
    {
        private readonly DbSet<Submission> _submissions;

        public BrowseSubmissionsHandler(AgendasDbContext context)
            => _submissions = context.Submissions;

        public async Task<IEnumerable<SubmissionDto>> HandleAsync(BrowseSubmissions query)
        {
            var submissions = _submissions
                .AsNoTracking()
                .Include(s => s.Speakers)
                .AsQueryable();

            if (query.ConferenceId.HasValue)
            {
                submissions = submissions.Where(x => x.ConferenceId == query.ConferenceId);
            }

            if (query.SpeakerId.HasValue)
            {
                submissions = submissions.Where(x => x.Speakers.Any(s => s.Id == query.SpeakerId));
            }

            return await submissions
                .Select(s => s.AsDto())
                .ToListAsync();
        }
    }
}