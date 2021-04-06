using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confab.Services.Tickets.Core.Entities;
using Confab.Services.Tickets.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Confab.Services.Tickets.Core.DAL.Repositories
{
    internal class TicketSaleRepository : ITicketSaleRepository
    {
        private readonly TicketsDbContext _context;
        private readonly DbSet<TicketSale> _ticketSales;

        public TicketSaleRepository(TicketsDbContext context)
        {
            _context = context;
            _ticketSales = _context.TicketSales;
        }

        public Task<TicketSale> GetAsync(Guid id)
            => _ticketSales
                .Include(x => x.Tickets)
                .FirstOrDefaultAsync(x => x.Id == id);

        public Task<TicketSale> GetCurrentForConferenceAsync(Guid conferenceId, DateTime now)
            => _ticketSales
                .Where(x => x.ConferenceId == conferenceId)
                .OrderBy(x => x.From)
                .Include(x => x.Tickets)
                .LastOrDefaultAsync(x => x.From <= now && x.To >= now);

        public async Task<IReadOnlyList<TicketSale>> BrowseForConferenceAsync(Guid conferenceId)
            => await _ticketSales
                .AsNoTracking()
                .Where(x => x.ConferenceId == conferenceId)
                .Include(x => x.Tickets)
                .ToListAsync();

        public async Task AddAsync(TicketSale ticketSale)
        {
            await _ticketSales.AddAsync(ticketSale);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TicketSale ticketSale)
        {
            _ticketSales.Update(ticketSale);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TicketSale ticketSale)
        {
            _ticketSales.Remove(ticketSale);
            await _context.SaveChangesAsync();
        }
    }
}