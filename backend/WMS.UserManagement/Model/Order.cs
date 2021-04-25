using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS.UserManagement.Model
{
    [Table("order")]
    public class Order
    {
        [Column("order_id")]
        public int Id { get; set; }
        [Column("company_id")]
        [Required]
        public Company Company { get; set; }
        [Column("order_name")]
        public string Name { get; set; }
        [Column("order_created_on")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedOn { get; }
        [Column("order_modified_on")]
        public DateTime ModifiedOn { get; }
    }
}
