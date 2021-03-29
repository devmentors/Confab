using Confab.Modules.Attendances.Domain.Entities;
using Confab.Modules.Attendances.Domain.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Confab.Modules.Attendances.Infrastructure.EF.Configurations
{
    internal class SlotConfiguration : IEntityTypeConfiguration<Slot>
    {
        public void Configure(EntityTypeBuilder<Slot> builder)
        {
            builder.Property(x => x.Id)
                .HasConversion(x => x.Value, x => new SlotId(x));

            builder.Property(x => x.ParticipantId)
                .HasConversion(x => x.Value, x => new ParticipantId(x))
                .IsConcurrencyToken();
        }
    }
}