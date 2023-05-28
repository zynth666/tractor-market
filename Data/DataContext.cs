using app.Entities;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace app.Data;

public class DataContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Tractor> Tractors => Set<Tractor>();
    public DbSet<TractorAddon> TractorAddons => Set<TractorAddon>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
        optionsBuilder.UseSqlite(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tractor>()
            .HasMany(e => e.Addons)
            .WithMany(e => e.Tractors)
            .UsingEntity("TractorToAddon");
    }
}
