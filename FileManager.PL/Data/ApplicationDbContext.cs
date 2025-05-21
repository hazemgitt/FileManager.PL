using FileManager.PL.Models;
using Microsoft.EntityFrameworkCore;

namespace FileManager.PL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<FileItem> Files { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure cascade delete for related entities
            modelBuilder.Entity<City>()
                .HasMany(c => c.Locations)
                .WithOne(l => l.City)
                .HasForeignKey(l => l.CityId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Location>()
                .HasMany(l => l.Folders)
                .WithOne(f => f.Location)
                .HasForeignKey(f => f.LocationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Folder>()
                .HasMany(f => f.Files)
                .WithOne(fi => fi.Folder)
                .HasForeignKey(fi => fi.FolderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
