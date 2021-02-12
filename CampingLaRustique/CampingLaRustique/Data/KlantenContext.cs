using Microsoft.EntityFrameworkCore;
using CampingLaRustique.Models;

namespace CampingLaRustique.Data
{
    public class KlantenContext : DbContext
    {
        public KlantenContext(DbContextOptions<KlantenContext> options)
            : base(options)
        {
        }

        public DbSet<Klanten> Klant { get; set; }

        public DbSet<Camping> Camping { get; set; }

        public DbSet<Reservering> Reservering { get; set; }
    }
    
}