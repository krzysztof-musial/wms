using WMS.UserManagement.Model.Db;

namespace WMS.UserManagement.Model.Authentication
{
    public class TokenValdiationResponse
    {
        public bool IsValid { get; set; }
        public User User { get; set; }
    }
}
