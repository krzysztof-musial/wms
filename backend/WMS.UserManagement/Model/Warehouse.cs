using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WMS.UserManagement.Model
{
    [Table("warehouse")]
    public class Warehouse
    {
        [Column("warehouse_id")]
        public int Id { get; set; }
        [Column("warehouse_name")]
        [Required]
        public string Name { get; set; }
        [Column("warehouse_created_on")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedOn { get; }
        [Column("warehouse_modified_on")]
        public DateTime ModifiedOn { get; }
    }
}
