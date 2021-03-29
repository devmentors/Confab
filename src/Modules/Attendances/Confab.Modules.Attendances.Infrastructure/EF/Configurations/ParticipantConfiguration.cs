using Confab.Modules.Attendances.Domain.Entities;
using Confab.Modules.Attendances.Domain.Types;
using Confab.Shared.Abstractions.Kernel.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Confab.Modules.Attendances.Infrastructure.EF.Configurations
{
    internal class ParticipantConfiguration : IEntityTypeConfiguration<Participant>
    {
        public void Configure(EntityTypeBuilder<Participant> builder)
        {
            builder.Property(x => x.Id)
                .HasConversion(x => x.Value, x => new ParticipantId(x));

            builder.Property(x => x.ConferenceId)
                .HasConversion(x => x.Value, x => new ConferenceId(x));
            
            builder.Property(x => x.UserId)
                .HasConversion(x => x.Value, x => new UserId(x));

            builder.HasIndex(x => new {x.UserId, x.ConferenceId}).IsUnique();
            builder.Property(x => x.Version).IsConcurrencyToken();
        }
    }
}