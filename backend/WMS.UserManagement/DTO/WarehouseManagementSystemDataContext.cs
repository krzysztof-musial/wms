
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WMS.UserManagement.Model;

namespace WMS.UserManagement.DTO
{
    public class WarehouseManagementSystemDataContext : IdentityDbContext<User>
    {
        public WarehouseManagementSystemDataContext(DbContextOptions<WarehouseManagementSystemDataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<UnitOfMessure> UnitOfMessures { get; set; }
    }
}
