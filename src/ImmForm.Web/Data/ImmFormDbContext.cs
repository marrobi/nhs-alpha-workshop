using Microsoft.EntityFrameworkCore;

namespace ImmForm.Web.Data;

public class ImmFormDbContext : DbContext
{
    public ImmFormDbContext(DbContextOptions<ImmFormDbContext> options)
        : base(options)
    {
    }

    public DbSet<Registration> Registrations => Set<Registration>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Registration>(entity =>
        {
            entity.HasIndex(e => e.CorrelationId).IsUnique();
            entity.HasIndex(e => e.Email);
            entity.Property(e => e.Status)
                .HasConversion<string>()
                .HasMaxLength(30);
        });

        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasIndex(e => e.RegistrationId);
            entity.HasIndex(e => e.CorrelationId);
            entity.HasIndex(e => e.EventType);
        });
    }
}
