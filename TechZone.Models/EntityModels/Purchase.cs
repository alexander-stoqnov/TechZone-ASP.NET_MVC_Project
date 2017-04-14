namespace TechZone.Models.EntityModels
{
    using System.Collections.Generic;
    using System;

    public class Purchase
    {
        public Purchase()
        {
            this.Products = new HashSet<Product>();
        }

        public int Id { get; set; }

        public DateTime PurchaseDate { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}