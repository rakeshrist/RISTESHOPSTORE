namespace WebAPI.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class tbl_category
    {
        [Key]
        public Guid category_id { get; set; }
        public string category_name { get; set; }

        public string parent_category { get; set; }
        public DateTime updated_on { get; set; }
        public int updated_by { get; set; }
    }
}