using System;
using System.Threading.Tasks;
using Confab.Modules.Tickets.Core.Entities;
using Confab.Modules.Tickets.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Tickets.Core.DAL.Repositories
{
    internal class ConferenceRepository : IConferenceRepository
    {
        private readonly TicketsDbContext _context;
        private readonly DbSet<Conference> _conferences;

        public ConferenceRepository(TicketsDbContext context)
        {
            _context = context;
            _conferences = _context.Conferences;
        }

        public Task<Conference> GetAsync(Guid id) => _conferences.SingleOrDefaultAsync(x => x.Id == id);

        public async Task AddAsync(Conference conference)
        {
            await _conferences.AddAsync(conference);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Conference conference)
        {
            _conferences.Update(conference);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Conference conference)
        {
            _conferences.Remove(conference);
            await _context.SaveChangesAsync();
        }
    }
}