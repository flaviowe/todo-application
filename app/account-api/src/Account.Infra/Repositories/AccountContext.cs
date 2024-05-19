using Account.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Account.Infra.Repositories;

public class AccountContext : DbContext
{
    public AccountContext(DbContextOptions<AccountContext> options) : base(options) {}

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        AddUser(modelBuilder);
    }

    private static void AddUser(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");

            AddId(entity);

            entity.Property(e => e.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Email)
                .HasColumnName("email")
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(e => e.Password)
                .HasColumnName("password")
                .IsRequired()
                .HasMaxLength(100);

            AddBaseEntity(entity);
        });
    }

    private static void AddId<T>(EntityTypeBuilder<T> entity) where T : BaseEntity
    {
        entity.Property(e => e.Id)
            .HasColumnName("id")
            .IsRequired();
    }

    private static void AddBaseEntity<T>(EntityTypeBuilder<T> entity) where T : BaseEntity
    {
        entity.Property(e => e.CreatedBy)
            .HasColumnName("created_by")
            .IsRequired();

        entity.Property(e => e.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        entity.Property(e => e.UpdateBy)
            .HasColumnName("update_by");

        entity.Property(e => e.UpdateAt)
            .HasColumnName("update_at");
    }
}