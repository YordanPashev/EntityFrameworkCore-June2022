namespace P03_FootballBetting.Data
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using P03_FootballBetting.Data.Models;

    public class FootballBettingContext : DbContext
    {
        public FootballBettingContext() { }


        public FootballBettingContext(DbContextOptions<FootballBettingContext> options)
            : base(options) { }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Color> Colors { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<PlayerStatistic> PlayerStatistics { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Bet> Bets { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-6A77MSO\\SQLEXPRESS;Database=FootballBetting;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayerStatistic>().
                HasKey(ps => new { ps.GameId, ps.PlayerId });

            modelBuilder.Entity<Team>(e =>
            {
                e.HasOne(t => t.PrimaryKitColor)
                .WithMany(c => c.PrimaryKitTeams)
                .HasForeignKey(t => t.PrimaryKitColorId)
                .OnDelete(DeleteBehavior.NoAction);

                e.HasOne(t => t.SecondaryKitColor)
                .WithMany(c => c.SecondaryKitTeams)
                .HasForeignKey(t => t.SecondaryKitColorId)
                .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Game>(e =>
            {
                e.HasOne(t => t.HomeTeam)
                .WithMany(c => c.HomeGames)
                .HasForeignKey(t => t.HomeTeamId)
                .OnDelete(DeleteBehavior.NoAction);

                e.HasOne(t => t.AwayTeam)
                .WithMany(c => c.AwayGames)
                .HasForeignKey(t => t.AwayTeamId)
                .OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}
