namespace TechZone.Models.BindingModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Enums;

    public class AddProductBindingModel
    {
        [Required]
        [MinLength(10, ErrorMessage = "Product name cannot be less than 10 characters")]
        [MaxLength(70, ErrorMessage = "Product name should be less than 50 characters")]
        public string Name { get; set; }

        [MinLength(30, ErrorMessage = "Product description cannot be less than 30 characters")]
        [MaxLength(3000, ErrorMessage = "Product description should be less than 3000 characters")]
        public string Description { get; set; }

        [RegularExpression("(http|https)://.+")]
        [Required]
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }

        [Range(0, Int64.MaxValue, ErrorMessage = "Price cannot be less than 0")]
        public decimal Price { get; set; }

        [Range(0, Int32.MaxValue, ErrorMessage = "Quantity cannot be less than 0")]
        public int Quantity { get; set; }

        [Range(0, 40, ErrorMessage = "Discount cannot be less than 0 and more than 40")]
        public int Discount { get; set; }

        public GuaranteeDurationType Guarantee { get; set; }
    }
}