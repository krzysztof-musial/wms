using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMS.UserManagement.DTO;
using WMS.UserManagement.Model.Common.Enums;
using WMS.UserManagement.Model.Common.Response;
using WMS.UserManagement.Model.Invitation;
using WMS.UserManagement.Model.Db;
using System;
using Microsoft.AspNetCore.Identity;

namespace WMS.UserManagement.Model.Services
{
    public class InvitationService : IInvitationService
    {
        private WarehouseManagementSystemDataContext _dbContext;
        private readonly UserManager<User> _userManager;
        public InvitationService(WarehouseManagementSystemDataContext dbContext, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<IResponse> AddInvitation(Db.Invitation invitation, int userId)
        {
            invitation.UserId = userId;
            _dbContext.Invitations.Add(invitation);
            await _dbContext.SaveChangesAsync();
            SuccessResponse<bool> successResponse = new SuccessResponse<bool>(true);
            return successResponse;
        }

        public async Task<IResponse> ApproveInvitation(SetInvitationStatus setInvitationStatus)
        {
            try
            {
                Db.Invitation invitation = await _dbContext.Invitations.FindAsync(setInvitationStatus.Id);
                IResponse response = await SetInvitationStatus(invitation, State.Approved);
                if (!response.Success)
                    return response;

                Model.Db.User user = await _userManager.FindByIdAsync(invitation.UserId.ToString());
                user.WarehouseId = invitation.WarehouseId;
                _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync();
                return response;
            }
            catch (Exception ex)
            {
                FailedResponse response = new FailedResponse(ex.ToString());
                return response;
            }
        }

        public async Task<IResponse> GetAllWarehouseActiveInvitations(int warehouseId)
        {
            var invitations = await _dbContext.Invitations.Where(x => x.WarehouseId == warehouseId && x.State == State.New).Include(x => x.User).ToListAsync();
            var response = new SuccessResponse<List<Db.Invitation>>(invitations);
            return response;
        }
        public async Task<IResponse> DeclineInvitation(SetInvitationStatus setInvitationStatus)
        {
            Db.Invitation invitation = await _dbContext.Invitations.FindAsync(setInvitationStatus.Id);
            IResponse response = await SetInvitationStatus(invitation, State.Rejected);
            if (!response.Success)
                return response;
            return response;
        }

        public async Task<IResponse> GetWarehouseInvitation(int warehouseId)
        {
            var invitations = await _dbContext.Invitations.Where(x => x.WarehouseId == warehouseId).Include(x => x.User).ToListAsync();
            var response = new SuccessResponse<List<Db.Invitation>>(invitations);
            return response;
        }
        private async Task<IResponse> SetInvitationStatus(Db.Invitation invitation, State state)
        {
            if (invitation == null)
            {
                FailedResponse response = InvitationResponse.GetInvitationNotFoundResponse();
                return response;
            }
            invitation.State = state;

            _dbContext.Invitations.Update(invitation);
            await _dbContext.SaveChangesAsync();

            SuccessResponse<Db.Invitation> successResponse = new SuccessResponse<Db.Invitation>(invitation);
            return successResponse;
        }

    }
}
