namespace SoftJail.Data
{
	
	using SoftJail.Data.Models;

	using Microsoft.EntityFrameworkCore;

    public class SoftJailDbContext : DbContext
	{
		public SoftJailDbContext() { }

		public SoftJailDbContext(DbContextOptions options)
			: base(options) { }

		public DbSet<Cell> Cells { get; set; }

		public DbSet<Department> Departments { get; set; }

		public DbSet<Mail> Mails { get; set; }

		public DbSet<Officer> Officers { get; set; }

		public DbSet<OfficerPrisoner> OfficersPrisoners { get; set; }

		public DbSet<Prisoner> Prisoners { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(Configuration.ConnectionString);
			}
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<OfficerPrisoner>().HasKey(op => new { op.PrisonerId, op.OfficerId });

			builder.Entity<Prisoner>().HasMany(op => op.PrisonerOfficers)
				                      .WithOne(op => op.Prisoner)
									  .HasForeignKey(op => op.PrisonerId)
							          .OnDelete(DeleteBehavior.NoAction);

			builder.Entity<Officer>().HasMany(op => op.OfficerPrisoners)
									  .WithOne(op => op.Officer)
									  .HasForeignKey(op => op.OfficerId)
									  .OnDelete(DeleteBehavior.NoAction);
		}
	}
}