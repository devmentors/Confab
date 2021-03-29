using Confab.Shared.Infrastructure.Postgres;

namespace Confab.Modules.Attendances.Infrastructure.EF
{
    internal class AttendancesUnitOfWork : PostgresUnitOfWork<AttendancesDbContext>, IAttendancesUnitOfWork
    {
        public AttendancesUnitOfWork(AttendancesDbContext dbContext) : base(dbContext)
        {
        }
    }
}