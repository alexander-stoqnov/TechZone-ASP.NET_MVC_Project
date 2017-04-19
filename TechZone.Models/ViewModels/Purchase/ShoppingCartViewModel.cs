namespace TechZone.Models.ViewModels.Purchase
{
    using System.Collections.Generic;

    public class ShoppingCartViewModel
    {
        public ShoppingCartViewModel()
        {
            this.ProductsInCart = new HashSet<ProductInCartViewModel>();
        }

        public IEnumerable<ProductInCartViewModel> ProductsInCart { get; set; }

        public decimal FinalPriceWithoutDiscount { get; set; }

        public decimal FinalPriceWithDiscount { get; set; }

        public decimal AmmountSaved { get; set; }
    }
}