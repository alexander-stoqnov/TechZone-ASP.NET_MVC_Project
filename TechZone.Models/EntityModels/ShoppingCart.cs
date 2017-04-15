namespace TechZone.Models.EntityModels
{
    using System.Collections.Generic;

    public class ShoppingCart
    {
        public ShoppingCart()
        {
            this.Products = new HashSet<Product>();
        }

        public int Id { get; set; }

        public ApplicationUser User { get; set; }

        public string Session { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}