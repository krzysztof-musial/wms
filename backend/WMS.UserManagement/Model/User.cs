using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS.UserManagement.Model
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Warehouse Warehouse { get; set; }
        [Column("email")]
        public override string Email { get => base.Email; set => base.Email = value; }
    }
}
