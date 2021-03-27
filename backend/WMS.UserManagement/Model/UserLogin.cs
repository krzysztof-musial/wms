using System.ComponentModel.DataAnnotations;

namespace WMS.UserManagement.Model
{
    public class UserLogin
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
