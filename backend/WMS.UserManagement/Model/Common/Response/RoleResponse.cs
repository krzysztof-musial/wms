namespace WMS.UserManagement.Model.Common.Response
{
    public class RoleResponse
    {
        public static FailedResponse GetRoleAssigningFailedResponse()
        {
            var response = new FailedResponse("The role assignment process failed. Please try again later");
            return response;
        }
    }
}
