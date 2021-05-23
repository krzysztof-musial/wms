using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS.UserManagement.Model.Db
{
    [Table("refresh_token")]
    public class RefreshToken
    {
        [Column("refresh_token_id")]
        [JsonIgnore]
        public int Id { get; set; }
        [Column("refresh_token_tokencontent")]
        [Required]
        public string Token { get; set; }
        [Column("refresh_token_created")]
        [Required]
        public DateTime Created { get; set; }
        [Column("refresh_token_expires")]
        [Required]
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        [Column("refresh_token_replaced_by_token")]
        public string ReplacedByToken { get; set; }
        [Column("refresh_token_revoked")]
        public DateTime? Revoked { get; set; }
        public bool IsActive => DateTime.UtcNow <= Expires; 
    }
}
