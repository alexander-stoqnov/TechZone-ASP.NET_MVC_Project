﻿namespace TechZone.Models.EntityModels
{
<<<<<<< HEAD
    using System.Collections.Generic;
    using System;
    using System.ComponentModel.DataAnnotations;
    using Enums;
    using Newtonsoft.Json;

=======
>>>>>>> parent of 60e339d... Added ASP.NET MVC
    public abstract class Product
    {
        protected Product()
        {
            this.Purchases = new HashSet<Purchase>();
            this.ShoppingCarts = new HashSet<ShoppingCart>();
            this.Reviews = new HashSet<Review>();
            this.IsAvailable = true;
        }

        public int Id { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Product name cannot be less than 10 characters")]
        [MaxLength(70, ErrorMessage = "Product name should be less than 50 characters")]
        public string Name { get; set; }

        [MinLength(30, ErrorMessage = "Product description cannot be less than 30 characters")]
        [MaxLength(3000, ErrorMessage = "Product description should be less than 3000 characters")]
        public string Description { get; set; }

        [RegularExpression("(http|https)://.+")]
        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [Range(0, Int64.MaxValue, ErrorMessage = "Price cannot be less than 0")]
        public decimal Price { get; set; }

        [Required]
        [Range(0, Int32.MaxValue, ErrorMessage = "Quantity cannot be less than 0")]
        public int Quantity { get; set; }

        [Required]
        [Range(0, 40, ErrorMessage = "Discount cannot be less than 0 and more than 40")]
        public int Discount { get; set; }

        [Range(0, Int32.MaxValue, ErrorMessage = "Views cannot be less than 0")]
        public int Views { get; set; }

        public bool IsAvailable { get; set; }

        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5 inclusive")]
        public decimal Rating { get; set; }

        public GuaranteeDurationType Guarantee { get; set; }

        [JsonIgnore]
        public virtual ICollection<Purchase> Purchases { get; set; }

        [JsonIgnore]
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }

        [JsonIgnore]
        public virtual ICollection<Review> Reviews { get; set; }
    }
}