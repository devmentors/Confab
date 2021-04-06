using System;
using System.ComponentModel.DataAnnotations;

namespace Confab.Services.Tickets.Core.DTO
{
    public class TicketSaleDto
    {
        public Guid Id { get; set; }
        public Guid ConferenceId { get; set; }
        
        public string Name { get; set; }
        
        [Range(0,100000)]
        public decimal? Price { get; set; }
        public int? Amount { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}