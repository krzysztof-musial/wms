﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS.UserManagement.Model
{
    [Table("article")]
    public class Article
    {
        [Column("article_id")]
        public int Id { get; set; }
        [Column("product_id")]
        [Required]
        public Product ProductId { get; set; }
        [Column("location_id")]
        [Required]
        public Location LocationId { get; set; }
        [Column("article_quantity")]
        [Required]
        public decimal Quantity { get; set; }
        [Column("article_created_on")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedOn { get; }
        [Column("article_modified_on")]
        public DateTime ModifiedOn { get; }
    }
}
