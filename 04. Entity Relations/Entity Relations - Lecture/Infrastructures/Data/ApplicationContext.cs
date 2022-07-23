namespace Infrastructures.Data
{

    using Infrastructures.Configurations;
    using Infrastructures.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationContext : DbContext
    {
        public ApplicationContext() { }


        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options) { }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Dog> Dogs { get; set; }

        public DbSet<Person> People { get; set; }

        public DbSet<PetShop> PetShops { get; set; }

        public DbSet<PetShop> PetShopsPoeple { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-6A77MSO\\SQLEXPRESS;Database=Pets;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DogConfig());
            modelBuilder.ApplyConfiguration(new PersonConfig());
        }
    }
}
