namespace WebAPI.Dtos
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CategoryDto
    {
        public Guid category_id { get; set; }
        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "{0} is madatory field")]
        [StringLength(50, MinimumLength = 2)]
        [RegularExpression(".*[a-zA-Z]+.*", ErrorMessage = "Only numerics are not allowed")]
        public string category_name { get; set; }
        [Display(Name = "Parent Category Name")]
        [Required(ErrorMessage = "{0} is madatory field")]
        public string parent_category { get; set; }
    }
}