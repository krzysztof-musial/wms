using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WMS.UserManagement.DTO;
using WMS.UserManagement.Model.Common.Response;
using WMS.UserManagement.Model.Db;
using WMS.UserManagement.Model.Services;

namespace WMS.UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompanyController : ControllerBase
    {
        private readonly WarehouseManagementSystemDataContext _dbContext;
        private readonly ICompanyService _companyService;
        public CompanyController(WarehouseManagementSystemDataContext dbContext, ICompanyService companyService)
        {
            _dbContext = dbContext;
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<ActionResult> GetWarehouseCompanies()
        {
            int warehouseId = GetUserWarehouseId();
            var response = await _companyService.GetWarehouseCompanies(warehouseId);
            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompany(int id)
        {
            var response = await _companyService.GetCompany(id);
            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpPost]
        public async Task<ActionResult> AddCompany([FromBody] Company company)
        {
            int warehouseId = GetUserWarehouseId();
            var response = await _companyService.AddCompany(company, warehouseId);
            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

        private int GetUserWarehouseId()
        {
            return int.Parse(User.Claims.FirstOrDefault(x => x.Type == "warehouseId").Value);
        }
    }
}
