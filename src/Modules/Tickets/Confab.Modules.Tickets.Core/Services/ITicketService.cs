using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confab.Modules.Tickets.Core.DTO;

namespace Confab.Modules.Tickets.Core.Services
{
    public interface ITicketService
    {
        Task PurchaseAsync(Guid conferenceId, Guid userId);
        Task<IEnumerable<TicketDto>> GetForUserAsync(Guid userId);
    }
}