using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WMS.UserManagement.Model.Common
{
    public interface IResponse
    {
        public bool Success { get; }
        public string Message { get; set; }
    }
}
