using System;
using System.Collections.Generic;

namespace Confab.Modules.Tickets.Core.Entities
{
    public class TicketSale
    {
        public Guid Id { get; set; }
        public Guid ConferenceId { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public int? Amount { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public IList<Ticket> Tickets { get; set; }
    }
}