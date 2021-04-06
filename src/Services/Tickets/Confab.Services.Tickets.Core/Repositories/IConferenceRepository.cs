using System;
using System.Threading.Tasks;
using Confab.Services.Tickets.Core.Entities;

namespace Confab.Services.Tickets.Core.Repositories
{
    internal interface IConferenceRepository
    {
        Task<Conference> GetAsync(Guid id);
        Task AddAsync(Conference conference);
        Task UpdateAsync(Conference conference);
        Task DeleteAsync(Conference conference);
    }
}