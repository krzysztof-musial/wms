using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using WMS.UserManagement.Model.Db;

namespace WMS.UserManagement.DTO
{
    public class WarehouseManagementSystemDataContext : IdentityDbContext<User, Role, int>
    {
        public WarehouseManagementSystemDataContext(DbContextOptions<WarehouseManagementSystemDataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            builder.Entity<User>().ToTable("user");
            builder.Entity<Role>().ToTable("role");
            builder.Entity<IdentityUserRole<int>>().ToTable("userrole");
            builder.Entity<IdentityRoleClaim<int>>().ToTable("roleclaim");
            builder.Entity<IdentityUserClaim<int>>().ToTable("userclaim");
            builder.Entity<IdentityUserLogin<int>>().ToTable("userlogin");
            builder.Entity<IdentityUserToken<int>>().ToTable("usertoken");
            //builder.ApplyConfiguration(new UserConfiguration());
            builder.Entity<User>()
                .HasOne(x => x.Warehouse)
                .WithMany();
            builder.Entity<User>()
                .HasIndex(x => x.WarehouseId);

            builder.Entity<Warehouse>()
                .HasOne(x => x.CreatedBy)
                .WithMany();
            builder.Entity<Warehouse>()
                .HasIndex(x => x.UserId)
                .IsUnique();
        }

        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<UnitOfMessure> UnitOfMessures { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
    }
}
