using Microsoft.EntityFrameworkCore;

namespace Web2.API.Models
{
    public class TP2A_Context : DbContext
    {
        public TP2A_Context(DbContextOptions<TP2A_Context> option) : base(option)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Evenement> Evenements { get; set; }
        public DbSet<Participation> Participations { get; set; }
        public DbSet<Ville> Villes { get; set; }

    }
}