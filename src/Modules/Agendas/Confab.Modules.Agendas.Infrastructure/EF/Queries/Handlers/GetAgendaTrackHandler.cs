using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.Agendas.DTO;
using Confab.Modules.Agendas.Application.Agendas.Queries;
using Confab.Modules.Agendas.Domain.Agendas.Entities;
using Confab.Modules.Agendas.Infrastructure.EF.Mappings;
using Confab.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Agendas.Infrastructure.EF.Queries.Handlers
{
    internal sealed class GetAgendaTrackHandler : IQueryHandler<GetAgendaTrack, AgendaTrackDto>
    {
        private readonly DbSet<AgendaTrack> _agendaTracks;

        public GetAgendaTrackHandler(AgendasDbContext context)
        {
            _agendaTracks = context.AgendaTracks;
        }

        public async Task<AgendaTrackDto> HandleAsync(GetAgendaTrack query)
        {
            var agendaTrack = await _agendaTracks
                .Include(at => at.Slots)
                .ThenInclude(at => (at as RegularAgendaSlot).AgendaItem)
                .ThenInclude(ai => ai.Speakers)
                .SingleOrDefaultAsync(x => x.Id == query.Id);

            return agendaTrack?.AsDto();
        }
    }
}