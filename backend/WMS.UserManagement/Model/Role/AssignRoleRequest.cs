using WMS.UserManagement.Model.Common.Enums;

namespace WMS.UserManagement.Model.Role
{
    public class AssignRoleRequest
    {
        public int UserId { get; set; }
        public RoleType Role { get; set; }
    }
}
