using System.Collections.Generic;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Domain.Agendas.Entities;
using Confab.Shared.Abstractions.Kernel.Types;

namespace Confab.Modules.Agendas.Domain.Agendas.Repositories
{
    public interface IAgendaTracksRepository
    {
        Task<AgendaTrack> GetAsync(AggregateId id);
        Task<IEnumerable<AgendaTrack>> BrowseAsync(ConferenceId conferenceId);
        Task<bool> ExistsAsync(AggregateId id);
        Task AddAsync(AgendaTrack agendaTrack);
        Task UpdateAsync(AgendaTrack agendaTrack);
        Task DeleteAsync(AgendaTrack agendaTrack);
    }
}