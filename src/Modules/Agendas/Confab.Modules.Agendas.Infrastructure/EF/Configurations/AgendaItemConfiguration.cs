using System;
using System.Collections.Generic;
using System.Linq;
using Confab.Modules.Agendas.Domain.Agendas.Entities;
using Confab.Shared.Abstractions.Kernel.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Confab.Modules.Agendas.Infrastructure.EF.Configurations
{
    public class AgendaItemConfiguration: IEntityTypeConfiguration<AgendaItem>
    {
        public void Configure(EntityTypeBuilder<AgendaItem> builder)
        {
            builder.HasKey(ai => ai.Id);
            
            builder
                .Property(ai => ai.Id)
                .HasConversion(id => id.Value, id => new AggregateId(id));
            
            builder
                .Property(ai => ai.ConferenceId)
                .HasConversion(id => id.Value, id => new ConferenceId(id));
            
            builder
                .Property(ai => ai.Tags)
                .HasConversion(tags => string.Join(',', tags), tags => tags.Split(',', StringSplitOptions.None));

            builder
                .Property(ai => ai.Version)
                .IsConcurrencyToken();
            
            builder
                .Property(x => x.Tags).Metadata.SetValueComparer(
                new ValueComparer<IEnumerable<string>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode()))));
        }
    }
}