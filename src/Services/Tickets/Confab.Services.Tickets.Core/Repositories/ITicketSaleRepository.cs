using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confab.Services.Tickets.Core.Entities;

namespace Confab.Services.Tickets.Core.Repositories
{
    internal interface ITicketSaleRepository
    {
        Task<TicketSale> GetAsync(Guid id);
        Task<TicketSale> GetCurrentForConferenceAsync(Guid conferenceId, DateTime now);
        Task<IReadOnlyList<TicketSale>> BrowseForConferenceAsync(Guid conferenceId);
        Task AddAsync(TicketSale ticketSale);
        Task UpdateAsync(TicketSale ticketSale);
        Task DeleteAsync(TicketSale ticketSale);
    }
}