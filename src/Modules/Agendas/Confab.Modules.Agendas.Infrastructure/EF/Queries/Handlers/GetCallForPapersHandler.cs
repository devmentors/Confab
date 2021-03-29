using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.CallForPapers.DTO;
using Confab.Modules.Agendas.Application.CallForPapers.Queries;
using Confab.Modules.Agendas.Domain.CallForPapers.Entities;
using Confab.Modules.Agendas.Infrastructure.EF.Mappings;
using Confab.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Agendas.Infrastructure.EF.Queries.Handlers
{
    internal sealed class GetCallForPapersHandler : IQueryHandler<GetCallForPapers, CallForPapersDto>
    {
        private readonly DbSet<CallForPapers> _callForPapers;

        public GetCallForPapersHandler(AgendasDbContext context)
            => _callForPapers = context.CallForPapers;

        public async Task<CallForPapersDto> HandleAsync(GetCallForPapers query)
            => await _callForPapers
                .AsNoTracking()
                .Where(cfp => cfp.ConferenceId == query.ConferenceId)
                .Select(cfp => cfp.AsDto())
                .SingleOrDefaultAsync();
    }
}