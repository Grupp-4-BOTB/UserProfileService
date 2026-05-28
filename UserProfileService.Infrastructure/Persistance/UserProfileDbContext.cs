using Microsoft.EntityFrameworkCore;
using UserProfileService.Domain.Entities;
using UserProfileService.Domain.ValueObjects;

namespace UserProfileService.Infrastructure.Persistance;

public class UserProfileDbContext : DbContext
{
    public UserProfileDbContext(DbContextOptions<UserProfileDbContext> options) : base(options) { }

    public DbSet<UserProfile> UserProfiles => Set<UserProfile>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.OwnerId)
                .HasConversion(v => v.Value, v => new OwnerId(v))
                .IsRequired();

            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20);

            entity.Property(e => e.Description)
                .HasMaxLength(500);

            entity.Property(e => e.PhotoUrl)
                .HasMaxLength(2048);
        });
    }
}
