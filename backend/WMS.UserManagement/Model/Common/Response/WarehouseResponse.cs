namespace WMS.UserManagement.Model.Common.Response
{
    public static class WarehouseResponse
    {
        public static FailedResponse GetWarehouseAlreadyExistsResponse()
        {
            var response = new FailedResponse("Provided name of warehouse already exists");
            return response;
        }
        public static FailedResponse GetUserAlreadyCreatedWarehouseResponse()
        {
            var response = new FailedResponse("You have already created a warehouse");
            return response;
        }

        public static FailedResponse GetUserIsAlreadyOwnerOfAnotherWarehouse()
        {
            var response = new FailedResponse("User, which you try to assign to your warehouse is already owner of another warehouse");
            return response;
        }
    }
}
