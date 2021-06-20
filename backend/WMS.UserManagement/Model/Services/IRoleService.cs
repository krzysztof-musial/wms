using System.Threading.Tasks;
using WMS.UserManagement.Model.Common.Response;
using WMS.UserManagement.Model.Role;

namespace WMS.UserManagement.Model.Services
{
    public interface IRoleService
    {
        public Task<IResponse> AssignRole(AssignRoleRequest assignRoleRequest);
    }
}
