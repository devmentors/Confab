using System.Threading.Tasks;
using Confab.Modules.Attendances.Domain.Entities;
using Confab.Modules.Attendances.Domain.Types;

namespace Confab.Modules.Attendances.Domain.Repositories
{
    public interface IAttendableEventsRepository
    {
        Task<AttendableEvent> GetAsync(AttendableEventId id);
        Task AddAsync(AttendableEvent attendableEvent);
        Task UpdateAsync(AttendableEvent attendableEvent);

    }
}