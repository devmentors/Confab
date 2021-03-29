using System.Collections.Generic;
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
    internal sealed class BrowseAgendaItemsHandler : IQueryHandler<BrowseAgendaItems, IEnumerable<AgendaItemDto>>
    {
        private readonly DbSet<AgendaItem> _agendaItems;

        public BrowseAgendaItemsHandler(AgendasDbContext context)
            => _agendaItems = context.AgendaItems;


        public async Task<IEnumerable<AgendaItemDto>> HandleAsync(BrowseAgendaItems query)
            => await _agendaItems
                .AsNoTracking()
                .Include(ai => ai.Speakers)
                .Where(ai => ai.ConferenceId == query.ConferenceId)
                .Select(ai => ai.AsDto())
                .ToListAsync();
    }
}