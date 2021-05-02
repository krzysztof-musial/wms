using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using WMS.UserManagement.Utils;

namespace WMS.UserManagement.Model.Authentication
{
    public class Registration
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PasswordConfirmation { get; set; }

        public bool WhetherPasswordsAreSame()
        {
            return Text.WhetherTextsAreSame(Password, PasswordConfirmation);
        }
    }
}
