using System;
using System.Net.Mail;

namespace WMS.UserManagement.Utils
{
    public static class EmailValidation
    {
        public static bool IsEmailCorrect(string email)
        {
            try
            {
                MailAddress mail = new MailAddress(email);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
