using Microsoft.EntityFrameworkCore;
using Web2.API.Data.Models;

namespace Web2.API.Data
{
    public class TP2A_Context : DbContext
    {
        public TP2A_Context(DbContextOptions<TP2A_Context> option) : base(option)
        {
            //AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);   // A Garder ?
        }

        public DbSet<Categorie> Categories { get; set; }
        public DbSet<Evenement> Evenements { get; set; }
        public DbSet<Participation> Participations { get; set; }
        public DbSet<Ville> Villes { get; set; }

    }
}