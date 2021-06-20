using System.Threading.Tasks;
using WMS.UserManagement.Model.Common.Response;
using WMS.UserManagement.Model.Db;
using WMS.UserManagement.Model.Invitation;

namespace WMS.UserManagement.Model.Services
{
    public interface IInvitationService
    {
        public Task<IResponse> GetWarehouseInvitation(int warehouseId);
        public Task<IResponse> GetAllWarehouseActiveInvitations(int warehouseId);
        public Task<IResponse> AddInvitation(Db.Invitation invitation, int userId);
        public Task<IResponse> ApproveInvitation(SetInvitationStatus setInvitationStatus);
        public Task<IResponse> DeclineInvitation(SetInvitationStatus setInvitationStatus);
    }
}
