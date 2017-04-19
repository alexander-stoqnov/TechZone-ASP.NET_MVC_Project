namespace TechZone.Models.EntityModels
{
    using System.Collections.Generic;
    using System;
    using System.ComponentModel.DataAnnotations;
    using Enums;

    public abstract class Product
    {
        protected Product()
        {
            this.Purchases = new HashSet<Purchase>();
            this.WishlistedBy = new HashSet<Customer>();
            this.ShoppingCarts = new HashSet<ShoppingCart>();
            this.IsAvailable = true;
        }

        public int Id { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Product name cannot be less than 10 characters long")]
        public string Name { get; set; }

        [MinLength(30, ErrorMessage = "Product description cannot be less than 30 characters long")]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        [Range(0, Int64.MaxValue, ErrorMessage = "Price cannot be less than 0")]
        public decimal Price { get; set; }

        [Range(0, Int32.MaxValue, ErrorMessage = "Quantity cannot be less than 0")]
        public int Quantity { get; set; }

        [Range(0, 60, ErrorMessage = "Discount cannot be less than 0 and more than 60")]
        public int Discount { get; set; }

        [Range(0, Int32.MaxValue, ErrorMessage = "Views cannot be less than 0")]
        public int Views { get; set; }

        public bool IsAvailable { get; set; }

        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5 inclusive")]
        public decimal Rating { get; set; }

        public GuaranteeDurationType Guarantee { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }

        public virtual ICollection<Customer> WishlistedBy { get; set; }

        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}