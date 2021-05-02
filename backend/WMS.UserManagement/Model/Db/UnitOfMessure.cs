using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS.UserManagement.Model.Db
{
    [Table("uom")]
    public class UnitOfMessure
    {
        [Column("uom_id")]
        public int Id { get; set; }
        [Column("uom_name")]
        public string Name { get; set; }
        [Column("uom_created_on")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedOn { get; }
        [Column("uom_modified_on")]
        public DateTime ModifiedOn { get; }
    }
}
