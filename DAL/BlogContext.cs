using DAL.Entities;
using DAL.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class BlogContext: IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public DbSet<Post> Post { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<PostCategory> PostCategory { get; set; }
    public DbSet<RefreshTokens> RefreshTokens { get; set; }
    public BlogContext(DbContextOptions<BlogContext> options)
        : base(options)
    {
    }
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        Guid ADMIN_ID = Guid.Parse("e4f1a6b3-1d6f-4424-a817-ea78db2e760f");
        Guid USER_ID = Guid.Parse("fce796d1-7c09-4299-bb2a-6a9ccfa39390");
        Guid USER_ADMIN_ID = Guid.Parse("90c50356-919f-4270-9cb4-6b3ea5dc4c64");
        
        modelBuilder.Entity<IdentityRole<Guid>>().HasData(
            new IdentityRole<Guid>
            {
                Name = Role.Admin.ToString(),
                NormalizedName = Role.Admin.ToString().ToUpper(),
                Id = ADMIN_ID,
                ConcurrencyStamp = ADMIN_ID.ToString()
            });
        
        modelBuilder.Entity<IdentityRole<Guid>>().HasData(
            new IdentityRole<Guid>
            {
                Name = Role.User.ToString(),
                NormalizedName = Role.User.ToString().ToUpper(),
                Id = USER_ID,
                ConcurrencyStamp = USER_ID.ToString()
            });
        
        var hasher = new PasswordHasher<User>();
        var admin = new User
        {
            Id = USER_ADMIN_ID,
            UserName = "igli.janko@gmail.com",
            Email = "igli.janko@gmail.com",
            FirstName = "igli",
            LastName = "janko",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            NormalizedEmail = "IGLI.JANKO@GMAIL.COM",
            NormalizedUserName = "IGLI.JANKO@GMAIL.COM",
            ConcurrencyStamp = USER_ADMIN_ID.ToString(),
            SecurityStamp = USER_ADMIN_ID.ToString()
        };

        admin.PasswordHash = hasher.HashPassword(admin, "User123.");
        
        modelBuilder.Entity<User>().HasData(admin);

        modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
        {
            RoleId = ADMIN_ID,
            UserId = USER_ADMIN_ID,
        });
        
        modelBuilder.Entity<PostCategory>()
            .HasKey(nc => new { nc.PostGuid, nc.CategoryGuid });
        
        modelBuilder.Entity<PostCategory>()
            .HasOne(pc => pc.Post)
            .WithMany(p => p.PostCategories)
            .HasForeignKey(pc => pc.PostGuid);

        modelBuilder.Entity<PostCategory>()
            .HasOne(pc => pc.Category)
            .WithMany(c => c.PostCategories)
            .HasForeignKey(pc => pc.CategoryGuid);
        
        modelBuilder.Entity<Category>()
            .Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();
        
        modelBuilder.Entity<Post>()
            .Property(n => n.Title)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<Post>()
            .Property(n => n.CreatedAt)
            .IsRequired();

        modelBuilder.Entity<Post>()
            .Property(n => n.UpdatedAt)
            .IsRequired();
        modelBuilder.Entity<RefreshTokens>()
            .HasOne(us => us.User)
            .WithMany(au => au.RefreshTokens)
            .HasForeignKey(us => us.UserId);
    }

}