using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confab.Services.Tickets.Core.DTO;

namespace Confab.Services.Tickets.Core.Services
{
    public interface ITicketSaleService
    {
        Task AddAsync(TicketSaleDto dto);
        Task<IEnumerable<TicketSaleInfoDto>> GetAllAsync(Guid conferenceId);
        Task<TicketSaleInfoDto> GetCurrentAsync(Guid conferenceId);
    }
}