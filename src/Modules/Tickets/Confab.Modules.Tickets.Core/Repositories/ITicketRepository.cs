using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confab.Modules.Tickets.Core.Entities;

namespace Confab.Modules.Tickets.Core.Repositories
{
    internal interface ITicketRepository
    {
        Task<Ticket> GetAsync(Guid conferenceId, Guid userId);
        Task<int> CountForConferenceAsync(Guid conferenceId);
        Task<IReadOnlyList<Ticket>> GetForUserAsync(Guid userId);
        Task AddAsync(Ticket ticket);
        Task AddManyAsync(IEnumerable<Ticket> ticket);
        Task UpdateAsync(Ticket ticket);
        Task DeleteAsync(Ticket ticket);
    }
}