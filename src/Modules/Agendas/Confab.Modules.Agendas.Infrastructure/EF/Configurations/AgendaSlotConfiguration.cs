using Confab.Modules.Agendas.Application.Agendas.Types;
using Confab.Modules.Agendas.Domain.Agendas.Entities;
using Confab.Shared.Abstractions.Kernel.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Confab.Modules.Agendas.Infrastructure.EF.Configurations
{
    public class AgendaSlotConfiguration : IEntityTypeConfiguration<AgendaSlot>
    {
        public void Configure(EntityTypeBuilder<AgendaSlot> builder)
        {
            builder.HasKey(s => s.Id);

            builder
                .Property(s => s.Id)
                .HasConversion(id => id.Value, id => new EntityId(id));
            
            builder
                .HasDiscriminator<string>("Type")
                .HasValue<PlaceholderAgendaSlot>(AgendaSlotType.Placeholder)
                .HasValue<RegularAgendaSlot>(AgendaSlotType.Regular);
        }
    }
}