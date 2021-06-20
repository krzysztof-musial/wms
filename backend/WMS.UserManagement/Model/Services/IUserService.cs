using System.Security.Claims;
using System.Threading.Tasks;
using WMS.UserManagement.Model.Authentication;
using WMS.UserManagement.Model.Common.Response;
using WMS.UserManagement.Model.Role;
using WMS.UserManagement.Model.Warehouse;

namespace WMS.UserManagement.Model.Services
{
    public interface IUserService
    {
        public Task<IResponse> Register(Registration registration);
        public Task<IResponse> Login(Login login, string ipAddress);
        public Task<IResponse> AssignRole(AssignRoleRequest assignRoleRequest);
        public Task<IResponse> AssignUserToWarehouse(AssignUserToWarehouse assignUserToWarehouse, ClaimsPrincipal userClaimsPrincipal);
        public Task<IResponse> ResetPassword(ResetPassword resetPassword);
        public Task<IResponse> RefreshToken(RefreshTokenRequest refreshTokenRequest);
        public Task<IResponse> RevokeToken(RevokeTokenRequest revokeTokenRequest, int userId);
        public Task<IResponse> IsTokenValid(string token);
    }
}
