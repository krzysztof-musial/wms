using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WMS.UserManagement.DTO;
using WMS.UserManagement.Model.Db;
using WMS.UserManagement.Model.Common.Response;
using WMS.UserManagement.Utils;
using WMS.UserManagement.Model.Services;
using WMS.UserManagement.Model.Role;

namespace WMS.UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WarehousesController : ControllerBase
    {
        private readonly WarehouseManagementSystemDataContext _dbContext;
        private readonly IUserService _userService;

        public WarehousesController(WarehouseManagementSystemDataContext context, IUserService userService)
        {
            _dbContext = context;
            _userService = userService;
        }

        // GET: api/Warehouses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Warehouse>>> GetWarehouses()
        {
            return await _dbContext.Warehouses.ToListAsync();
        }

        [HttpGet]
        [Route("GetWarehouseMembers/{id}")]
        public async Task<ActionResult<IEnumerable<User>>> GetWarehouseMembers(int id)
        {
            return await _dbContext.Users.Where(x => x.WarehouseId == id).ToListAsync();
        }

        // GET: api/Warehouses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Warehouse>> GetWarehouse(int id)
        {
            var warehouse = await _dbContext.Warehouses.FindAsync(id);

            if (warehouse == null)
            {
                return NotFound();
            }

            return warehouse;
        }

        // PUT: api/Warehouses/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWarehouse(int id, Warehouse warehouse)
        {
            if (id != warehouse.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(warehouse).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WarehouseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Warehouses
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SuccessResponse<Warehouse>>> PostWarehouse(Warehouse warehouse)
        {
            int userId = UserClaims.GetUserIdFromClaims(User);
            User user = _dbContext.Users.Where(x => x.Id == userId).FirstOrDefault();
            var userHasCreatedWarehouse = _dbContext.Warehouses.Any(x => x.UserId == userId);
            if (userHasCreatedWarehouse)
            {
                FailedResponse failerdResponse = WarehouseResponse.GetUserAlreadyCreatedWarehouseResponse();
                return BadRequest(failerdResponse);
            }

            var warehouseAlreadyExists = _dbContext.Warehouses.Any(x => x.Name == warehouse.Name);
            if (warehouseAlreadyExists)
            {
                FailedResponse failerdResponse = WarehouseResponse.GetWarehouseAlreadyExistsResponse();
                return BadRequest(failerdResponse);
            }
            warehouse.UserId = userId;
            var addedWarehouse = _dbContext.Warehouses.Add(warehouse);
            await _dbContext.SaveChangesAsync();

            user.WarehouseId = addedWarehouse.Entity.Id;
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();

            AssignRoleRequest assignRoleRequest = new AssignRoleRequest()
            {
                UserId = userId,
                Role = Model.Common.Enums.RoleType.Owner
            };
            var response = await _userService.AssignRole(assignRoleRequest);
            if (!response.Success)
                return BadRequest(response);

            SuccessResponse<Warehouse> successResponse = new SuccessResponse<Warehouse>(warehouse);
            return Ok(successResponse);
        }

        private bool WarehouseExists(int id)
        {
            return _dbContext.Warehouses.Any(e => e.Id == id);
        }
    }
}
