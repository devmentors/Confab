using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.Agendas.DTO;
using Confab.Modules.Agendas.Application.Agendas.Queries;
using Confab.Modules.Agendas.Domain.Agendas.Entities;
using Confab.Modules.Agendas.Infrastructure.EF.Mappings;
using Confab.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Agendas.Infrastructure.EF.Queries.Handlers
{
    internal sealed class GetAgendaItemHandler : IQueryHandler<GetAgendaItem, AgendaItemDto>
    {
        private readonly DbSet<AgendaItem> _agendaItems;

        public GetAgendaItemHandler(AgendasDbContext context)
            => _agendaItems = context.AgendaItems;

        public async Task<AgendaItemDto> HandleAsync(GetAgendaItem query)
            => await _agendaItems
                .AsNoTracking()
                .Include(ai => ai.Speakers)
                .Where(ai => ai.Id == query.Id)
                .Select(ai => ai.AsDto()).FirstOrDefaultAsync();
    }
}