using System;

namespace WMS.UserManagement.Model.Common
{
    public class SuccessResponse<T> : IResponse
    {
        public bool Success { get; private set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public SuccessResponse(T data)
        {
            Success = true;
            Data = data;
        }
        public SuccessResponse(string message, T data)
        {
            Success = true;
            Message = message;
            Data = data;
        }
    }
}
