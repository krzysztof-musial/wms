
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS.UserManagement.Model.Db
{
    [Table("company")]
    public class Company
    {
        [Column("company_id")]
        public int Id { get; set; }
        [Column("company_name")]
        [Required]
        public string Name { get; set; }
        [Column("column_tin")]
        [Required]
        public ulong Tin { get; set; }
        [Column("company_street")]
        [Required]
        public string Street { get; set; }
        [Column("company_country")]
        [Required]
        public string Country { get; set; }
        [Column("company_postal_code")]
        [Required]
        public string PostalCode { get; set; }
        [Column("company_created_on")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedOn { get; }
        [Column("company_modified_on")]
        public DateTime ModifiedOn { get; }
        [Column("company_warehouse_id")]
        [Required]
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
    }
}
