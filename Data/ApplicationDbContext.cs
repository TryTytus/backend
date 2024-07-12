using backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace backend;

public partial class ApplicationDbContext 
//: IdentityDbContext
     : IdentityDbContext<AspNetUser, AspNetRole, int, AspNetUserClaim, AspNetUserRole , AspNetUserLogin, AspNetRoleClaim, AspNetUserToken>
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        // this.ChangeTracker.LazyLoadingEnabled = false;
    }

    public DbSet<AspNetRole> AspNetRoles { get; set; }

    public DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public DbSet<AspNetUser> AspNetUsers { get; set; }

    public DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Database=twitter;Username=postgres;Password=example");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex").IsUnique();

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasDefaultValueSql("''::character varying");
            entity.Property(e => e.Nickname)
                .HasMaxLength(15)
                .HasDefaultValueSql("''::character varying");
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany<Post>().WithOne().HasForeignKey(e => e.UserId);

            // entity.HasMany(d => d.Roles).WithMany(p => p.Users)
            //     .UsingEntity<Dictionary<string, object>>(
            //         "AspNetUserRole",
            //         r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
            //         l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
            //         j =>
            //         {
            //             j.HasKey("UserId", "RoleId");
            //             j.ToTable("AspNetUserRoles");
            //             j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
            //         });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.Property(e => e.CommentsCount).HasDefaultValue(0);
            entity.Property(e => e.Content).HasMaxLength(280);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp(3) without time zone");
            entity.Property(e => e.LikesCount).HasDefaultValue(0);
            entity.Property(e => e.UpdatedAt).HasColumnType("timestamp(3) without time zone");
            entity.Property(e => e.ViewsCount).HasDefaultValue(0);

            entity.HasOne(d => d.User).WithMany(p => p.Posts).HasForeignKey(d => d.UserId);
        });
        
        
    }
    
}
