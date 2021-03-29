using Confab.Modules.Agendas.Domain.Agendas.Entities;
using Confab.Shared.Abstractions.Kernel.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Confab.Modules.Agendas.Infrastructure.EF.Configurations
{
    internal class AgendaTrackConfiguration : IEntityTypeConfiguration<AgendaTrack>
    {
        public void Configure(EntityTypeBuilder<AgendaTrack> builder)
        {
            builder.HasKey(s => s.Id);
            
            builder
                .Property(s => s.Id)
                .HasConversion(id => id.Value, id => new AggregateId(id));
            
            builder
                .Property(s => s.ConferenceId)
                .HasConversion(id => id.Value, id => new ConferenceId(id));

            builder
                .HasMany(at => at.Slots)
                .WithOne(s => s.Track);

            builder
                .Property(at => at.Version)
                .IsConcurrencyToken();
        }
    }
}