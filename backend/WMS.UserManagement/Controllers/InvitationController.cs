using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMS.UserManagement.DTO;
using WMS.UserManagement.Model.Common.Enums;
using WMS.UserManagement.Model.Common.Response;
using WMS.UserManagement.Model.Db;
using WMS.UserManagement.Model.Invitation;
using WMS.UserManagement.Utils;

namespace WMS.UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InvitationController : ControllerBase
    {
        private readonly WarehouseManagementSystemDataContext _dbContext;
        private readonly UserManager<User> _userManager;
        public InvitationController(WarehouseManagementSystemDataContext dbContext, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult> GetWarehouseInvitation()
        {
            int warehouseId = GetUserWarehouseId();
            var invitations = await _dbContext.Invitations.Where(x => x.WarehouseId == warehouseId).Include(x => x.User).ToListAsync();
            var response = new SuccessResponse<List<Invitation>>(invitations);
            return Ok(response);
        }

        [HttpGet("GetAllWarehouseActiveInvitations")]
        public async Task<ActionResult> GetAllWarehouseActiveInvitations()
        {
            int warehouseId = GetUserWarehouseId();
            var invitations = await _dbContext.Invitations.Where(x => x.WarehouseId == warehouseId && x.State == State.New).Include(x => x.User).ToListAsync();
            var response = new SuccessResponse<List<Invitation>>(invitations);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> AddInvitation([FromBody] Invitation invitation)
        {
            int userId = UserClaims.GetUserIdFromClaims(User);
            invitation.UserId = userId;
            _dbContext.Invitations.Add(invitation);
            await _dbContext.SaveChangesAsync();
            SuccessResponse<bool> successResponse = new SuccessResponse<bool>(true);
            return Ok(invitation);
        }

        [HttpPost("ApproveInvitation")]
        public async Task<ActionResult> ApproveInvitation(SetInvitationStatus setInvitationStatus)
        {

            try
            {
                Invitation invitation = await _dbContext.Invitations.FindAsync(setInvitationStatus.Id);
                IResponse response = await SetInvitationStatus(invitation, State.Approved);
                if (!response.Success)
                    return BadRequest(response);


                Model.Db.User user = await _userManager.FindByIdAsync(invitation.UserId.ToString());
                user.WarehouseId = invitation.WarehouseId;
                _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                FailedResponse response = new FailedResponse(ex.ToString());
                return BadRequest(response);
            }
        }

        [HttpPost("DeclineInvitation")]
        public async Task<ActionResult> DeclineInvitation(SetInvitationStatus setInvitationStatus)
        {
            Invitation invitation = await _dbContext.Invitations.FindAsync(setInvitationStatus.Id);
            IResponse response = await SetInvitationStatus(invitation, State.Rejected);
            if (!response.Success)
                return BadRequest(response);
            return Ok(response);
        }

        private async Task<IResponse> SetInvitationStatus(Invitation invitation, State state)
        {
            if(invitation == null)
            {
                FailedResponse response = InvitationResponse.GetInvitationNotFoundResponse();
                return response;
            }
            invitation.State = state;

            _dbContext.Invitations.Update(invitation);
            await _dbContext.SaveChangesAsync();

            SuccessResponse<Invitation> successResponse = new SuccessResponse<Invitation>(invitation);
            return successResponse;
        }

        private int GetUserWarehouseId()
        {
            return int.Parse(User.Claims.FirstOrDefault(x => x.Type == "warehouseId").Value);
        }
    }
}
