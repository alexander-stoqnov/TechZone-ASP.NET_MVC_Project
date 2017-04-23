﻿namespace TechZone.Services
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
                    productVm.FinalPrice = CalculateFinalPrice(product.Discount, product.Price);
                }
                if (product.IsAvailable)
                {
                    productVms.Add(productVm);
                }
            }
            return productVms;
        }

        public bool ProductExists(int id)
        {
            return this.Context.Products.Find(id) != null;
        }

        public ProductDetailsViewModel GetProductDetails(int id)
        {
            var product = this.Context.Products.Find(id);
            product.Views++;
            var productDetailsVm = Mapper.Map<ProductDetailsViewModel>(product);
            if (product.Discount != 0)
            {
                productDetailsVm.FinalPrice = CalculateFinalPrice(product.Discount, product.Price);
            }
            this.Context.SaveChanges();
            return productDetailsVm;
        }

        private decimal CalculateFinalPrice(int discount, decimal price)
        {
            decimal discountFinal = discount / 100.0m;
            return price - price * discountFinal;
        }

        public Dictionary<string, string> GetProductSpecs(int id)
        {
            Dictionary<string, string> specs = new Dictionary<string, string>();

            if (ProductIsGraphicCard(id))
            {
                var graphicCard = this.Context.GraphicCards.Find(id);
                specs["Brand"] = graphicCard.Brand.ToString("G");
                specs["Manufacturer"] = graphicCard.Manufacturer.ToString("G");
                specs["Memory Type"] = graphicCard.MemoryType.ToString("G");
                specs["Memory Size"] = graphicCard.Memory.ToString();
                return specs;
            }

            if (ProductIsHardDrive(id))
            {
                var hardDrive = this.Context.HardDrives.Find(id);
                specs["Brand"] = hardDrive.DriveBrand.ToString("G");
                specs["Type"] = hardDrive.DriveType.ToString("G");
                specs["Capacity (Gb)"] = hardDrive.Capacity.ToString("G");
            }
            return specs;
        }

        private bool ProductIsGraphicCard(int id)
        {
            return this.Context.GraphicCards.FirstOrDefault(g => g.Id == id) != null;
        }

        private bool ProductIsHardDrive(int id)
        {
            return this.Context.HardDrives.FirstOrDefault(hd => hd.Id == id) != null;
        }
    }
}