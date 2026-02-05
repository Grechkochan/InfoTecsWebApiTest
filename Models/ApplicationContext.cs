using Microsoft.EntityFrameworkCore;
using InfotecsTestWebApi.Models.Entities;
namespace InfotecsTestWebApi.Models;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {

    }
    public DbSet<FileEntity> Files { get; set; }
    public DbSet<ValuesEntity> Values { get; set; }
    public DbSet<ResultsEntity> Results { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FileEntity>()
            .HasMany(f => f.Results)
            .WithOne(r => r.File)
            .HasForeignKey(r => r.FileId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<FileEntity>()
            .HasMany(f => f.Values)
            .WithOne(v => v.File)
            .HasForeignKey(v => v.FileId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<FileEntity>()
            .HasIndex(f => f.FileName)
            .IsUnique();
    }
}
