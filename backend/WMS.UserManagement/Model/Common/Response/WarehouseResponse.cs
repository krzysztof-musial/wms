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

        public static FailedResponse GetUserIsAlreadyOwnerOfAnotherWarehouseResponse()
        {
            var response = new FailedResponse("User, which you try to assign to your warehouse is already owner of another warehouse");
            return response;
        }

        public static FailedResponse GetUserIsAlreadyAssignedToAnotherWarehouseResponse()
        {
            var response = new FailedResponse("User, which you try to assign to your warehouse is already assigned to another warehouse");
            return response;
        }

        public static FailedResponse GetUserIsNotOwnerOfAnyWarehouseResponse()
        {
            var response = new FailedResponse("You aren't owner of any warehouse");
            return response;
        }
    }
}
