using KonusarakOgrenCase.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KonusarakOgrenCase.Persistence;

public class KonusarakOgrenCaseContext : DbContext
{
    public KonusarakOgrenCaseContext(DbContextOptions<KonusarakOgrenCaseContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        //builder.Entity<Product>().HasData(
        //    new Product
        //    {
        //        Id = 1,
        //        IsActive = true,
        //        CreatedAt = DateTime.Now,
        //        Name = "Dummy Product 1",
        //        UpdatedAt = DateTime.Now
        //    },
        //    new Product
        //    {
        //        Id = 2,
        //        IsActive = true,
        //        CreatedAt = DateTime.Now,
        //        Name = "Dummy Product 2",
        //        UpdatedAt = DateTime.Now
        //    }
        //);

        builder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                IsActive = true,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Mail = "string",
                HashedPassword = "lKFWcBKMHQj6XoT3q/4J3AuU+nG8bogGHhcWI1iHpXo=",
                Salt = "/FNGQLJtrqHksQMfsn+ZYA==",
                Name = "Bugra",
                Surname = "Durukan",
                UserRole = UserRole.Sysadmin
            }
        );

        //builder.Entity<Cart>().HasData(
        //    new Cart
        //    {
        //        Id = 1,
        //        IsActive = true,
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        UserId = 1
        //    }
        //);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        var entities = ChangeTracker.Entries().Where(x =>
            x.Entity is BaseEntity && x.State is EntityState.Added or EntityState.Modified);
        foreach (var entity in entities)
        {
            if (entity.State == EntityState.Added)
            {
                ((BaseEntity) entity.Entity).CreatedAt = DateTime.UtcNow;
                ((BaseEntity) entity.Entity).IsActive = true;
            }

            ((BaseEntity) entity.Entity).UpdatedAt = DateTime.UtcNow;
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}