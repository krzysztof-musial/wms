
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS.UserManagement.Model
{
    [Table("order_line")]
    public class OrderLine
    {
        [Column("order_line_id")]
        public int Id { get; set; }
        [Column("order_id")]
        [Required]
        public Order Order { get; set; }
        [Column("product_id")]
        [Required]
        public Product Product { get; set; }
        [Column("order_line_quantity")]
        [Required]
        public decimal Quantity { get; set; }
        [Column("order_line_created_on")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedOn { get; }
        [Column("order_line_modified_on")]
        public DateTime ModifiedOn { get; }
    }
}
