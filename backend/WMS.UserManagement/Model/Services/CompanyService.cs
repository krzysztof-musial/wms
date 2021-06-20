using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMS.UserManagement.DTO;
using WMS.UserManagement.Model.Common.Response;
using WMS.UserManagement.Model.Db;

namespace WMS.UserManagement.Model.Services
{
    public class CompanyService : ICompanyService
    {
        private WarehouseManagementSystemDataContext _dbContext;
        public CompanyService(WarehouseManagementSystemDataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IResponse> AddCompany(Company company, int warehouseId)
        {
            bool companyAlreadyExists = _dbContext.Companies.Any(x => x.Tin == company.Tin);
            if (companyAlreadyExists)
            {
                FailedResponse failedResponse = CompanyResponse.GetCompanyWithProvidedTinAlreadyExistsResponse();
                return failedResponse;
            }

            company.WarehouseId = warehouseId;
            _dbContext.Companies.Add(company);
            await _dbContext.SaveChangesAsync();
            SuccessResponse<Company> successResponse = new SuccessResponse<Company>(company);
            return successResponse;
        }

        public async Task<IResponse> GetCompany(int id)
        {
            var company = await _dbContext.Companies.FindAsync(id);

            if (company == null)
            {
                var response = CompanyResponse.GetCompanyDoesNotExistResponse();
                return response;
            }
            SuccessResponse<Company> successResponse = new SuccessResponse<Company>(company);
            return successResponse;
        }

        public async Task<IResponse> GetWarehouseCompanies(int warehouseId)
        {
            var companies = await _dbContext.Companies.Where(x => x.WarehouseId == warehouseId).ToListAsync();
            SuccessResponse<List<Company>> successResponse = new SuccessResponse<List<Company>>(companies);
            return successResponse;
        }
    }
}
