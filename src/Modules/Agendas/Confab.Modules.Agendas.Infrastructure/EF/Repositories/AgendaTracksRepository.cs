using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Domain.Agendas.Entities;
using Confab.Modules.Agendas.Domain.Agendas.Repositories;
using Confab.Shared.Abstractions.Kernel.Types;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Agendas.Infrastructure.EF.Repositories
{
    internal sealed class AgendaTracksRepository : IAgendaTracksRepository
    {
        private readonly AgendasDbContext _context;
        private readonly DbSet<AgendaTrack> _agendaTracks;

        public AgendaTracksRepository(AgendasDbContext context)
        {
            _context = context;
            _agendaTracks = context.AgendaTracks;
        }

        public async Task<AgendaTrack> GetAsync(AggregateId id)
            => await _agendaTracks.Include(at => at.Slots).SingleOrDefaultAsync(at => at.Id == id);

        public async Task<IEnumerable<AgendaTrack>> BrowseAsync(ConferenceId conferenceId)
            => await _agendaTracks.AsNoTracking().Include(at => at.Slots)
                .Where(at => at.ConferenceId == conferenceId).ToListAsync();

        public async Task<bool> ExistsAsync(AggregateId id)
            => await _agendaTracks.AnyAsync(at => at.Id == id);

        public async Task AddAsync(AgendaTrack agendaTrack)
        {
            await _agendaTracks.AddAsync(agendaTrack);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AgendaTrack agendaTrack)
            => await _context.SaveChangesAsync();

        public async Task DeleteAsync(AgendaTrack agendaTrack)
        {
            _agendaTracks.Remove(agendaTrack);
            await _context.SaveChangesAsync();
        }
    }
}