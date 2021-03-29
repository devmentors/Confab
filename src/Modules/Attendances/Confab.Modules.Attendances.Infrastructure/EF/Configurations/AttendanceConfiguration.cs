using Confab.Modules.Attendances.Domain.Entities;
using Confab.Modules.Attendances.Domain.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Confab.Modules.Attendances.Infrastructure.EF.Configurations
{
    internal class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
    {
        public void Configure(EntityTypeBuilder<Attendance> builder)
        {
            builder.Property(x => x.AttendableEventId)
                .HasConversion(x => x.Value, x => new AttendableEventId(x));
            
            builder.Property(x => x.SlotId)
                .HasConversion(x => x.Value, x => new SlotId(x));
            
            builder.Property(x => x.ParticipantId)
                .HasConversion(x => x.Value, x => new ParticipantId(x));
        }
    }
}