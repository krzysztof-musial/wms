using System.ComponentModel.DataAnnotations;

namespace WMS.UserManagement.Model
{
    public class UserLogin
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
