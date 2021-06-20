using WMS.UserManagement.Model.Common.Enums;

namespace WMS.UserManagement.Model.Role
{
    public class AssignRoleResponse
    {
        public RoleType Role { get; set; }
        public int UserId { get; set; }
    }
}
