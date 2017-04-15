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

        public virtual ApplicationUser Customer { get; set; }

        public string SessionId { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}