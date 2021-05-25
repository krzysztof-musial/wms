﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WMS.UserManagement.DTO;
using WMS.UserManagement.Model.Common.Response;
using WMS.UserManagement.Model.Db;

namespace WMS.UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompanyController : ControllerBase
    {
        private readonly WarehouseManagementSystemDataContext _dbContext;
        private readonly UserManager<User> _userManager;
        public CompanyController(WarehouseManagementSystemDataContext dbContext, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult> GetWarehouseCompanies()
        {
            var companies = await _dbContext.Companies.ToListAsync();
            SuccessResponse<List<Company>> successResponse = new SuccessResponse<List<Company>>(companies);
            return Ok(successResponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompany(int id)
        {
            var company = await _dbContext.Companies.FindAsync(id);

            if (company == null)
            {
                return NotFound();
            }
            SuccessResponse<Company> successResponse = new SuccessResponse<Company>(company);
            return Ok(successResponse);
        }

        [HttpPost]
        public async Task<ActionResult> AddCompany([FromBody] Company company)
        {
            bool companyAlreadyExists = _dbContext.Companies.Any(x => x.Tin == company.Tin);
            if(companyAlreadyExists)
            {
                FailedResponse failedResponse = CompanyResponse.GetCompanyWithProvidedTinAlreadyExistsResponse();
                return BadRequest(failedResponse);
            }
            _dbContext.Companies.Add(company);
            await _dbContext.SaveChangesAsync();
            SuccessResponse<Company> successResponse = new SuccessResponse<Company>(company);
            return Ok(successResponse);
        }
    }
}