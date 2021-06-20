using System.Threading.Tasks;
using WMS.UserManagement.Model.Common.Response;
using WMS.UserManagement.Model.Db;

namespace WMS.UserManagement.Model.Services
{
    public interface ICompanyService
    {
        public Task<IResponse> GetWarehouseCompanies(int warehouseId);
        public Task<IResponse> GetCompany(int id);
        public Task<IResponse> AddCompany(Company company, int warehouseId);
    }
}
