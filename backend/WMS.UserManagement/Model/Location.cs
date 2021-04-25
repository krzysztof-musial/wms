using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WMS.UserManagement.Model
{
    [Table("location")]
    public class Location
    {
        [Column("location_id")]
        public int Id { get; set; }
        [Column("warehouse_id")]
        [Required]
        public Warehouse Warehouse { get; set; }
        [Column("location_code")]
        [Required]
        public string Code { get; set; }
        [Column("location_created_on")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]

        public DateTime CreatedOn { get; }
        [Column("location_modified_on")]

        public DateTime ModifiedOn { get; }
    }
}
