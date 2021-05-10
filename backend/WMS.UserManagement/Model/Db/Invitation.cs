using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using WMS.UserManagement.Model.Common.Enums;

namespace WMS.UserManagement.Model.Db
{
    [Table("invitation")]
    public class Invitation
    {
        [Column("invitation_id")]
        public int Id { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        [Column("invitation_user_id")]
        [JsonIgnore]
        public User User { get; set; }
        public int WarehouseId { get; set; }
        [ForeignKey("WarehouseId")]
        [Column("invitation_warehouse_id")]
        [JsonIgnore]
        public Warehouse Warehouse { get; set; }
        [Column("invitation_state")]
        public State State { get; set; }
    }
}
