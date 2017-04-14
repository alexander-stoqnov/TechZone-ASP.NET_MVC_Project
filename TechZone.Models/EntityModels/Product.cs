namespace TechZone.Models.EntityModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Products")]
    public class Product
    {
        public Product()
        {
            this.Purchases = new HashSet<Purchase>();
            this.WishlistedBy = new HashSet<Customer>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int Discount { get; set; }

        public decimal Rating { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }

        public virtual ICollection<Customer> WishlistedBy { get; set; }
    }
}