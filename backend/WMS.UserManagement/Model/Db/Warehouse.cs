using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS.UserManagement.Model.Db
{
    [Table("warehouse")]
    public class Warehouse
    {
        [Column("warehouse_id")]
        public int Id { get; set; }
        [Column("warehouse_name")]
        [Required]
        public string Name { get; set; }
        [DataType("datetime2")]
        [Column("warehouse_created_on")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedOn { get; }
        [Column("warehouse_modified_on", TypeName = "datetime2" )]
        public DateTime ModifiedOn { get; }

        [Column("warehouse_user_id")]
        public int? UserId { get; set; }
        [JsonIgnore]
        public User CreatedBy { get; set; }
    }
}
