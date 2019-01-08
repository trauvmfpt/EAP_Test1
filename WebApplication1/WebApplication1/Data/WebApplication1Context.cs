using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class WebApplication1Context : DbContext
    {
        public WebApplication1Context (DbContextOptions<WebApplication1Context> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>().HasData(
                new { Id = "USD", Ratio = 23260},
                new { Id = "EUR", Ratio = 27061 },
                new { Id = "AUD", Ratio = 16798 },
                new { Id = "JPY", Ratio = 20704 }
                );
        }

        public DbSet<WebApplication1.Models.Currency> Currency { get; set; }
    }
}
