namespace ORM_Fundamentals.Data
{
    using Microsoft.EntityFrameworkCore;
    using ORM_Fundamentals.Data.Models;

    public class SoftUniDBContext : DbContext
    {
        public SoftUniDBContext() { }

        public SoftUniDBContext(DbContextOptions dbContextoptions)
            : base(dbContextoptions) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Config.ConnectionString);
            }
        }
    }
}
