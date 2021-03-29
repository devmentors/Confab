using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Domain.Submissions.Entities;
using Confab.Modules.Agendas.Domain.Submissions.Repositories;
using Confab.Shared.Abstractions.Kernel.Types;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Agendas.Infrastructure.EF.Repositories
{
    internal sealed class SpeakerRepository : ISpeakerRepository
    {
        private readonly AgendasDbContext _context;
        private readonly DbSet<Speaker> _speakers;

        public SpeakerRepository(AgendasDbContext context)
        {
            _context = context;
            _speakers = context.Speakers;
        }

        public Task<bool> ExistsAsync(AggregateId id)
            => _speakers.AnyAsync(x => x.Id.Equals(id));

        public async Task<IEnumerable<Speaker>> BrowseAsync(IEnumerable<AggregateId> ids)
            => await _speakers.Where(x => ids.Contains(x.Id)).ToListAsync();

        public async Task AddAsync(Speaker speaker)
        {
            await _speakers.AddAsync(speaker);
            await _context.SaveChangesAsync();
        }
    }
}