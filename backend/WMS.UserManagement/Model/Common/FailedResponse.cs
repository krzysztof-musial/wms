using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WMS.UserManagement.Model.Common
{
    public class FailedResponse : IResponse
    {
        public bool Success { get; private set; }
        public string Message { get; set; }
        public FailedResponse()
        {
            Success = false;
        }
        public FailedResponse(string message)
        {
            Success = false;
            Message = message;
        }
    }
}
