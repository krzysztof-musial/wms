namespace WMS.UserManagement.Model.Common.FailedResponses
{
    public static class WarehouseResponse
    {
        public static FailedResponse GetWarehouseAlreadyExistsResponse()
        {
            var response = new FailedResponse("Provided name of warehouse already exists");
            return response;
        }
    }
}
