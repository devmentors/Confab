using Confab.Services.Tickets.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Confab.Services.Tickets.Core.DAL
{
    internal class TicketsDbContext : DbContext
    {
        public DbSet<Conference> Conferences { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketSale> TicketSales { get; set; }
        
        public TicketsDbContext(DbContextOptions<TicketsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}