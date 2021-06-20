using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WMS.UserManagement.DTO;
using WMS.UserManagement.Model.Common.Response;
using WMS.UserManagement.Model.Db;
using WMS.UserManagement.Model.Role;
using WMS.UserManagement.Model.Services;

namespace WMS.UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoleController : ControllerBase
    {
        private readonly WarehouseManagementSystemDataContext _dbContext;
        private readonly IUserService _userService;

        public RoleController(WarehouseManagementSystemDataContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }
        [HttpPost("AssignRole")]
        [Authorize(Roles = RoleKind.Owner)]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequest assignRoleRequest)
        {
            var response = await _userService.AssignRole(assignRoleRequest);
            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }
    }
}
