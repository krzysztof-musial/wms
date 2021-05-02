using WMS.UserManagement.Model.Authentication;

namespace WMS.UserManagement.Model.Common.Response
{
    public static class UserResponse
    {
        public static FailedResponse GetWrongAuthDataResponse()
        {
            var response = new FailedResponse("Provided user or password is incorrect");
            return response;
        }

        public static FailedResponse GetUserIsAlreadyRegisteredResponse()
        {
            var response = new FailedResponse("User is already registered");
            return response;
        }
        public static FailedResponse GetNotSamePasswordsResponse()
        {
            var response = new FailedResponse("Provided passwords aren't the same");
            return response;
        }
        public static FailedResponse GetUserNotFoundErrorResponse()
        {
            var response = new FailedResponse("User not found");
            return response;
        }
        public static FailedResponse GetPropertyCannotBeNullResponse(string propertyName)
        {
            var response = new FailedResponse($"{propertyName} cannot be null");
            return response;
        }
        public static IResponse GetCreatedUserResponse(int userId)
        {
            RegistrationResult createdUser = new RegistrationResult()
            {
                UserId = userId
            };
            var response = new SuccessResponse<RegistrationResult>(createdUser);
            return response;
        }
    }
}
