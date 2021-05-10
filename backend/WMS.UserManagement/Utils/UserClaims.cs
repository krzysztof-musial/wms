using System.Linq;
using System.Security.Claims;

namespace WMS.UserManagement.Utils
{
    public class UserClaims
    {
        public static int GetUserIdFromClaims(ClaimsPrincipal user)
        {
            string id = user.Claims.Where(x => x.Type == "userId").FirstOrDefault()?.Value;
            int userId = int.Parse(id);
            return userId;
        }
    }
}
