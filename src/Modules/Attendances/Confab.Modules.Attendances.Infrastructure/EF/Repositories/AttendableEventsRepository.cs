using System.Threading.Tasks;
using Confab.Modules.Attendances.Domain.Entities;
using Confab.Modules.Attendances.Domain.Repositories;
using Confab.Modules.Attendances.Domain.Types;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Attendances.Infrastructure.EF.Repositories
{
    internal class AttendableEventsRepository : IAttendableEventsRepository
    {
        private readonly AttendancesDbContext _context;
        private readonly DbSet<AttendableEvent> _attendableEvents;

        public AttendableEventsRepository(AttendancesDbContext context)
        {
            _context = context;
            _attendableEvents = context.AttendableEvents;
        }

        public Task<AttendableEvent> GetAsync(AttendableEventId id)
            => _attendableEvents
                .Include(x => x.Slots)
                .SingleOrDefaultAsync(s => s.Id == id);

        public async Task AddAsync(AttendableEvent attendableEvent)
        {
            await _attendableEvents.AddAsync(attendableEvent);
            await _context.SaveChangesAsync();
        }
        
        public async Task UpdateAsync(AttendableEvent attendableEvent)
        {
            _attendableEvents.Update(attendableEvent);
            await _context.SaveChangesAsync();
        }
    }
}