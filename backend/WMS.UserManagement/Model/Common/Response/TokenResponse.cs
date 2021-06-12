namespace WMS.UserManagement.Model.Common.Response
{
    public static class TokenResponse
    {
        public static FailedResponse GetRefreshTokenIsNotValidResponse()
        {
            var response = new FailedResponse("Provided refresh token is incorrect");
            return response;
        }

        public static FailedResponse GetRefreshTokenIsExpiredResponse()
        {
            var response = new FailedResponse("Provided refresh token is expired");
            return response;
        }

        public static FailedResponse GetAccessTokenIsNotValidResponse()
        {
            var response = new FailedResponse("Provided access token is not valid");
            return response;
        }
        public static FailedResponse GetAccessTokenIsNotValidResponse(string message)
        {
            var response = new FailedResponse(message);
            return response;
        }
    }
}
