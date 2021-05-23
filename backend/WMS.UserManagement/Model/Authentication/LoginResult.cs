using WMS.UserManagement.Model.Db;

namespace WMS.UserManagement.Model.Authentication
{
    public class LoginResult
    {
        public string Token { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
}
