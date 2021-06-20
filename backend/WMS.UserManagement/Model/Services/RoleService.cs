using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using WMS.UserManagement.DTO;
using WMS.UserManagement.Model.Common.Response;
using WMS.UserManagement.Model.Role;

namespace WMS.UserManagement.Model.Services
{
    public class RoleService : IRoleService
    {
        private WarehouseManagementSystemDataContext _dbContext;

        public RoleService(WarehouseManagementSystemDataContext dbContext)
        {
            _dbContext = dbContext;

        }
        public async Task<IResponse> AssignRole(AssignRoleRequest assignRoleRequest)
        {
            if (assignRoleRequest == null)
            {
                FailedResponse response = RoleResponse.GetMissingPropertiesFailedResponse();
                return response;
            }

            var user = _dbContext.Users.FirstOrDefault(x => x.Id == assignRoleRequest.UserId);

            if (user == null)
            {
                FailedResponse response = UserResponse.GetUserNotFoundErrorResponse();
                return response;
            }

            user.Role = assignRoleRequest.Role;
            user.RoleKind = assignRoleRequest.Role.ToString();
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
            var assignRoleResponse = new AssignRoleResponse()
            {
                Role = assignRoleRequest.Role,
                UserId = assignRoleRequest.UserId
            };
            var successResponse = new SuccessResponse<AssignRoleResponse>(assignRoleResponse);
            return successResponse;
        }

    }
}
