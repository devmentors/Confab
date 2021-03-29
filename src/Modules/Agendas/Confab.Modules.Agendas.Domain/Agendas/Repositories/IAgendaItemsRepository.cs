using System.Collections.Generic;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Domain.Agendas.Entities;
using Confab.Shared.Abstractions.Kernel.Types;

namespace Confab.Modules.Agendas.Domain.Agendas.Repositories
{
    public interface IAgendaItemsRepository
    {
        Task<IEnumerable<AgendaItem>> BrowseAsync(IEnumerable<SpeakerId> speakerIds);
        Task<AgendaItem> GetAsync(AggregateId id);
        Task AddAsync(AgendaItem agendaItem);
        Task UpdateAsync(AgendaItem agendaItem);
        Task DeleteAsync(AgendaItem agendaItem);
    }
}