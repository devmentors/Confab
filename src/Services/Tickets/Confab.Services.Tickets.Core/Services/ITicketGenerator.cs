using System;
using Confab.Services.Tickets.Core.Entities;

namespace Confab.Services.Tickets.Core.Services
{
    public interface ITicketGenerator
    {
        Ticket Generate(Guid conferenceId, Guid ticketSaleId, decimal? price);
    }
}