using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS.UserManagement.Model
{
    public class UserToken : IdentityUserToken<int>
    {
        [Column("usertoken_id")]
        public int Id { get; set; }
    }
}
