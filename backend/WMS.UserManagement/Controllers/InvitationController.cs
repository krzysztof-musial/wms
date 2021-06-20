using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WMS.UserManagement.DTO;
using WMS.UserManagement.Model.Db;
using WMS.UserManagement.Model.Invitation;
using WMS.UserManagement.Model.Role;
using WMS.UserManagement.Model.Services;

namespace WMS.UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InvitationController : ControllerBase
    {
        private readonly WarehouseManagementSystemDataContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly IInvitationService _invitationService;
        public InvitationController(WarehouseManagementSystemDataContext dbContext, UserManager<User> userManager, IInvitationService invitationService)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _invitationService = invitationService;
        }

        [Authorize(Roles = RoleKind.Owner + "," + RoleKind.Manager)]
        [HttpGet]
        public async Task<ActionResult> GetWarehouseInvitation()
        {
            int warehouseId = GetUserWarehouseId();
            var response = await _invitationService.GetWarehouseInvitation(warehouseId);
            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }


        [Authorize(Roles = RoleKind.Owner + "," + RoleKind.Manager)]
        [HttpGet("GetAllWarehouseActiveInvitations")]
        public async Task<ActionResult> GetAllWarehouseActiveInvitations()
        {
            int warehouseId = GetUserWarehouseId();
            var response = await _invitationService.GetAllWarehouseActiveInvitations(warehouseId);
            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

        [Authorize(Roles = RoleKind.Owner + "," + RoleKind.Manager)]
        [HttpPost]
        public async Task<ActionResult> AddInvitation([FromBody] Invitation invitation)
        {
            int userId = GetUserWarehouseId();
            var response = await _invitationService.AddInvitation(invitation, userId);
            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

        [Authorize(Roles = RoleKind.Owner + "," + RoleKind.Manager)]
        [HttpPost("ApproveInvitation")]
        public async Task<ActionResult> ApproveInvitation(SetInvitationStatus setInvitationStatus)
        {
            var response = await _invitationService.ApproveInvitation(setInvitationStatus);
            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

        [Authorize(Roles = RoleKind.Owner + "," + RoleKind.Manager)]
        [HttpPost("DeclineInvitation")]
        public async Task<ActionResult> DeclineInvitation(SetInvitationStatus setInvitationStatus)
        {
            var response = await _invitationService.DeclineInvitation(setInvitationStatus);
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
