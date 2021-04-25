using System.ComponentModel.DataAnnotations;

namespace WMS.UserManagement.Model.Authentication
{
    public class Login
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
