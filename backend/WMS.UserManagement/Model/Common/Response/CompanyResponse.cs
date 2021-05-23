using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WMS.UserManagement.Model.Common.Response
{
    public class CompanyResponse
    {
        public static FailedResponse GetCompanyWithProvidedTinAlreadyExistsResponse()
        {
            var response = new FailedResponse("Company with provided tin already exists");
            return response;
        }

        public static FailedResponse GetCompanyDoesNotExistResponse()
        {
            var response = new FailedResponse("Company with provided id doesn't exist");
            return response;
        }
    }
}
