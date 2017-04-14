namespace TechZone.Models.EntityModels
{
    using System.Collections.Generic;

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

        public int Discount { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }

        public virtual ICollection<Customer> WishlistedBy { get; set; }
    }
}