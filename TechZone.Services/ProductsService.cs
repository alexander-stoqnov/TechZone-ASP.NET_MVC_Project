namespace TechZone.Services
{
    using System.Collections.Generic;
    using Models.ViewModels.Products;
    using System.Linq;
    using AutoMapper;

    public class ProductsService : Service
    {
        public IEnumerable<GeneralProductPageViewModel> GetAllProducts()
        {
            var products = this.Context.Products.ToList();
            var productVms = new HashSet<GeneralProductPageViewModel>();
            foreach (var product in products)
            {
                var productVm = Mapper.Map<GeneralProductPageViewModel>(product);
                if (productVm.Name.Length > 35)
                {
                    productVm.Name = productVm.Name.Substring(0, 35) + "...";
                }
                if (product.Discount != 0)
                {
                    decimal discountFinal = product.Discount / 100.0m;
                    decimal finalPrice = product.Price - product.Price * discountFinal;
                    productVm.FinalPrice = finalPrice;
                }
                productVms.Add(productVm);
            }
            return productVms;
        }
    }
}