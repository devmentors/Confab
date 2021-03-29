using Confab.Modules.Attendances.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Attendances.Infrastructure.EF
{
    public sealed class AttendancesDbContext : DbContext
    {
        public DbSet<AttendableEvent> AttendableEvents { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Slot> Slots { get; set; }

        public AttendancesDbContext(DbContextOptions<AttendancesDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("attendances");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}