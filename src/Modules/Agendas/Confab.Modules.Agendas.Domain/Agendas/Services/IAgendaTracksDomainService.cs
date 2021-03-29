using System.Threading.Tasks;
using Confab.Modules.Agendas.Domain.Agendas.Entities;
using Confab.Shared.Abstractions.Kernel.Types;

namespace Confab.Modules.Agendas.Domain.Agendas.Services
{
    public interface IAgendaTracksDomainService
    {
        Task AssignAgendaItemAsync(AgendaTrack agendaTrack, EntityId agendaSlotId, EntityId agendaItemId);
    }
}