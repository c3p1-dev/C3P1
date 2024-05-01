using C3P1.Client.Components.Apps.VisualCarnet;
using Microsoft.EntityFrameworkCore;

namespace C3P1.Data
{
    public class VisualCarnetContext(DbContextOptions<VisualCarnetContext> options) : DbContext(options)
    {
        public DbSet<FicCpt> FicCpts { get; set; }

        public DbSet<FicFam> FicFams { get; set; }

        public DbSet<FicSfa> FicSfas { get; set; }

        public DbSet<WrkEcrLig> WrkEcrLigs { get; set; }
    }
}
