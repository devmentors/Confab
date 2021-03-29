using System;
using Confab.Modules.Tickets.Core.Entities;

namespace Confab.Modules.Tickets.Core.Services
{
    public interface ITicketGenerator
    {
        Ticket Generate(Guid conferenceId, Guid ticketSaleId, decimal? price);
    }
}