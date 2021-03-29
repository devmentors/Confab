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
    internal sealed class GetRegularAgendaSlotHandler : IQueryHandler<GetRegularAgendaSlot, RegularAgendaSlotDto>
    {
        private readonly DbSet<AgendaSlot> _agendaSlots;

        public GetRegularAgendaSlotHandler(AgendasDbContext context)
        {
            _agendaSlots = context.AgendaSlots;
        }
        
        public async Task<RegularAgendaSlotDto> HandleAsync(GetRegularAgendaSlot query)
        {
            var slot = await _agendaSlots
                .OfType<RegularAgendaSlot>()
                .Include(x => x.AgendaItem)
                .ThenInclude(x => x.Speakers)
                .SingleOrDefaultAsync(x => x.AgendaItem.Id == query.AgendaItemId);

            return slot?.AsDto();
        }
    }
}