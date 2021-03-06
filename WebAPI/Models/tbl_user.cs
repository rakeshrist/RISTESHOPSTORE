using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class tbl_user
    {
        [Key]
        public Guid id { get; set; }
        [Required]
        public string user_name { get; set; }
        [Required]
        public byte[] password { get; set; }
        public byte[] password_key { get; set; }
        public string access_token { get; set; }
    }
}