using Microsoft.EntityFrameworkCore;
using FutbolFan1.Models;

namespace FutbolFan1.Data
{
    public class FutbolFanContext : DbContext
    {
        public FutbolFanContext(DbContextOptions<FutbolFanContext> options)
            : base(options)
        {
        }

        // Add your DbSet properties for custom entities
        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<TeamSave> TeamSaves { get; set; }
        public DbSet<PlayerSave> PlayerSaves { get; set; }
        public DbSet<Formation> Formations { get; set; }
        public DbSet<Championship> Championships { get; set; }
        public DbSet<ChampionshipTeam> ChampionshipTeams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Call base method for EF Core

            // Additional configurations for custom entities
            modelBuilder.Entity<TeamSave>()
                .HasOne(ts => ts.Team)
                .WithMany(t => t.TeamSaves)
                .HasForeignKey(ts => ts.TeamId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PlayerSave>()
                .HasOne(ps => ps.Player)
                .WithMany(p => p.PlayerSaves)
                .HasForeignKey(ps => ps.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PlayerSave>()
                .HasOne(ps => ps.TeamSave)
                .WithMany(ts => ts.PlayerSaves)
                .HasForeignKey(ps => ps.TeamSaveId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Team>()
                .HasOne(t => t.CurrentFormation)
                .WithMany(f => f.Teams)
                .HasForeignKey(t => t.CurrentFormationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ChampionshipTeam>()
                .HasKey(ct => new { ct.ChampionshipId, ct.TeamId });

            modelBuilder.Entity<ChampionshipTeam>()
                .HasOne(ct => ct.Championship)
                .WithMany(c => c.ChampionshipTeams)
                .HasForeignKey(ct => ct.ChampionshipId);

            modelBuilder.Entity<ChampionshipTeam>()
                .HasOne(ct => ct.Team)
                .WithMany(t => t.ChampionshipTeams)
                .HasForeignKey(ct => ct.TeamId);
        }
    }
}
