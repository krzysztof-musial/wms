namespace WMS.UserManagement.Model.Common.Response
{
    public class InvitationResponse
    {
        public static FailedResponse GetInvitationNotFoundResponse()
        {
            FailedResponse response = new FailedResponse("Invitation with provided id doesn't exist");
            return response;
        }
    }
}
