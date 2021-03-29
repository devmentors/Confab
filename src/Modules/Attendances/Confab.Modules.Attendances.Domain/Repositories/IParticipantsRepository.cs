using System.Threading.Tasks;
using Confab.Modules.Attendances.Domain.Entities;
using Confab.Modules.Attendances.Domain.Types;
using Confab.Shared.Abstractions.Kernel.Types;

namespace Confab.Modules.Attendances.Domain.Repositories
{
    public interface IParticipantsRepository
    {
        Task<Participant> GetAsync(ParticipantId id);
        Task<Participant> GetAsync(ConferenceId conferenceId, UserId userId);
        Task AddAsync(Participant participant);
        Task UpdateAsync(Participant participant);
    }
}