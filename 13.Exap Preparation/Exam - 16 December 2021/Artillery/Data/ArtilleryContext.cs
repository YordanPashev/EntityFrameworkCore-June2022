namespace Artillery.Data
{
    using Microsoft.EntityFrameworkCore;

    using Artillery.Data.Models;

    public class ArtilleryContext : DbContext
    {
        public ArtilleryContext() { }

        public ArtilleryContext(DbContextOptions options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }

        public DbSet<Gun> Guns { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<CountryGun> CountriesGuns { get; set; }

        public DbSet<Shell> Shells { get; set; }

        public DbSet<Manufacturer> Manufacturers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CountryGun>(cg =>
            cg.HasKey(cg => new { cg.CountryId, cg.GunId }));
        }
    }
}
