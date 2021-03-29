using Confab.Modules.Attendances.Domain.Entities;
using Confab.Modules.Attendances.Domain.Types;
using Confab.Shared.Abstractions.Kernel.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Confab.Modules.Attendances.Infrastructure.EF.Configurations
{
    internal class AttendableEventConfiguration : IEntityTypeConfiguration<AttendableEvent>
    {
        public void Configure(EntityTypeBuilder<AttendableEvent> builder)
        {
            builder.Property(x => x.Id)
                .HasConversion(x => x.Value, x => new AttendableEventId(x));

            builder.Property(x => x.ConferenceId)
                .HasConversion(x => x.Value, x => new ConferenceId(x));
        }
    }
}