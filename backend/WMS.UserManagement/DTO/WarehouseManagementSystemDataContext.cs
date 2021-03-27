
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
    }
}
