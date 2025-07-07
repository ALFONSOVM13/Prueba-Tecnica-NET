using Microsoft.EntityFrameworkCore;
using TravelRequests.Domain.Entities;

namespace TravelRequests.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<TravelRequest> TravelRequests { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TravelRequest>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.OriginCity).HasMaxLength(100).IsRequired();
            entity.Property(e => e.DestinationCity).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Justification).HasMaxLength(500).IsRequired();
            entity.HasOne(e => e.User)
                  .WithMany(u => u.TravelRequests)
                  .HasForeignKey(e => e.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Email).HasMaxLength(100).IsRequired();
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.PasswordHash).HasMaxLength(100).IsRequired();
            entity.Property(e => e.PasswordResetCode).HasMaxLength(100);
        });
    }
} 