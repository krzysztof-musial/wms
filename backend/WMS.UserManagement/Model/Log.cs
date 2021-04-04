using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS.UserManagement.Model
{
    [Table("log")]
    public class Log
    {
        [Column("log_id")]
        public int Id { get; set; }
        [Column("user_id")]
        [Required]
        public int UserId { get; set; }
        [Column("warehouse_id")]
        [Required]
        public int WarehouseId { get; set; }
        [Column("log_message")]
        [Required]
        public string Message { get; set; }
        [Column("log_created_on")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedOn { get; }
        [Column("log_modified_on")]
        public DateTime ModifiedOn { get; }
    }
}
