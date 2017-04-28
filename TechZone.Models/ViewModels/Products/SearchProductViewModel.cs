namespace TechZone.Models.ViewModels.Products
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SearchProductViewModel
    {
        [Required]
        [MinLength(10, ErrorMessage = "Product name cannot be less than 10 characters long")]
        public string Name { get; set; }

        [Required]
        [Range(0, Int64.MaxValue, ErrorMessage = "Price cannot be less than 0")]
        public decimal Price { get; set; }

        [Required]
        [Range(0, 40, ErrorMessage = "Discount cannot be less than 0 and more than 40")]
        public int Discount { get; set; }
    }
}