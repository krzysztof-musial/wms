
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS.UserManagement.Model.Db
{
    [Table("product")]
    public class Product
    {
        [Column("product_id")]
        public int Id { get; set; }
        [Column("warehouse_id")]
        [Required]
        public Warehouse Warehouse { get; set; }
        [Column("company_id")]
        [Required]
        public Company Company { get; set; }
        [Column("uom_id")]
        [Required]
        public UnitOfMessure UnitOfMessure { get; set; }
        [Column("product_name")]
        [Required]
        public string Name { get; set; }
        [Column("product_code")]
        [Required]
        public string Code { get; set; }
        [Column("product_description")]
        public string Description { get; set; }
        [Column("uom_created_on")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedOn { get; }
        [Column("product_modified_on")]
        public DateTime ModifiedOn { get; }
    }
}
