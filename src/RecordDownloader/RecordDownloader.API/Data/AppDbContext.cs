using Microsoft.EntityFrameworkCore;
using RecordDownloader.Models;

namespace RecordDownloader.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<RecordEntity> Records { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RecordEntity>(entity =>
        {
            entity.ToTable("record", "public");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Filename).HasColumnName("filename");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.TextContent).HasColumnName("text");
        });
    }
}