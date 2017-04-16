namespace TechZone.Models.ViewModels.Purchase
{
    using System.Collections.Generic;

    public class ShoppingCartViewModel
    {
        public ShoppingCartViewModel()
        {
            this.ProductsInCart = new HashSet<ProductInCartViewModel>();
        }

        public int Id { get; set; }

        public ICollection<ProductInCartViewModel> ProductsInCart { get; set; }
    }
}