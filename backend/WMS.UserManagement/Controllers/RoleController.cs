using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WMS.UserManagement.DTO;
using WMS.UserManagement.Model.Common.Response;
using WMS.UserManagement.Model.Db;
using WMS.UserManagement.Model.Role;

namespace WMS.UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoleController : ControllerBase
    {
        private readonly WarehouseManagementSystemDataContext _dbContext;
        private readonly UserManager<User> _userManager;

        public RoleController(WarehouseManagementSystemDataContext dbContext, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequest assignRoleRequest)
        {
            // TO DO - dodawać do access tokena roli
            if(assignRoleRequest == null)
            {
                FailedResponse response = RoleResponse.GetMissingPropertiesFailedResponse();
                return BadRequest(response);
            }

            var user = _dbContext.Users.FirstOrDefault(x => x.Id == assignRoleRequest.UserId);

            if(user == null)
            {
                FailedResponse response = UserResponse.GetUserNotFoundErrorResponse();
                return BadRequest(response);
            }

            user.Role = assignRoleRequest.Role;
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
