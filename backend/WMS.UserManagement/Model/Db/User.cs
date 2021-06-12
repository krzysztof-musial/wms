using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WMS.UserManagement.Model.Authentication;
using WMS.UserManagement.Model.Common.Enums;

namespace WMS.UserManagement.Model.Db
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
        public List<RefreshToken> RefreshToken { get; set; }
        [JsonIgnore]
        public override string SecurityStamp { get; set; }
        [JsonIgnore]
        public override string PasswordHash { get; set; }
        public RoleType Role { get; set; }
    }
}
