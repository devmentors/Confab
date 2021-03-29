using System;
using Confab.Modules.Attendances.Infrastructure.EF;
using Confab.Shared.Tests;

namespace Confab.Modules.Attendances.Tests.Integration.Common
{
    public class TestAttendancesDbContext : IDisposable
    {
        public AttendancesDbContext DbContext { get; }

        public TestAttendancesDbContext()
        {
            DbContext = new AttendancesDbContext(DbHelper.GetOptions<AttendancesDbContext>());
        }

        public void Dispose()
        {
            DbContext?.Database.EnsureDeleted();
            DbContext?.Dispose();
        }
    }
}