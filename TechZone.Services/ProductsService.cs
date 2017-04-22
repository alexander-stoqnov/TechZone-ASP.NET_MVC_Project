using System;
using TechZone.Models.ViewModels.Reviews;

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

        public bool ProductIsGraphicCard(int id)
        {
            return this.Context.GraphicCards.FirstOrDefault(g => g.Id == id) != null;
        }

        public bool ProductIsHardDrive(int id)
        {
            return this.Context.HardDrives.FirstOrDefault(hd => hd.Id == id) != null;
        }

        public GraphicCardSpecsViewModel GetGraphicSpecs(int id)
        {
            return Mapper.Map<GraphicCardSpecsViewModel>(this.Context.GraphicCards.Find(id));
        }

        public HardDriveSpecsViewModel GetHardDriveSpecs(int id)
        {
            return Mapper.Map<HardDriveSpecsViewModel>(this.Context.HardDrives.Find(id));
        }

        public ReviewOverviewViewModel GetReviewsForProduct(int id)
        {
            var product = this.Context.Products.Find(id);
            ReviewOverviewViewModel rovm = new ReviewOverviewViewModel
            {
                AverageUserRating = (decimal) product.Reviews.Average(r => r.Rating),
                NumberOf1Star = product.Reviews.Count(r => r.Rating == 1),
                NumberOf2Stars = product.Reviews.Count(r => r.Rating == 2),
                NumberOf3Stars = product.Reviews.Count(r => r.Rating == 3),
                NumberOf4Stars = product.Reviews.Count(r => r.Rating == 4),
                NumberOf5Stars = product.Reviews.Count(r => r.Rating == 5),
            };

            var reviews = product.Reviews.ToList();
            foreach (var review in reviews)
            {
                SimpleReviewViewModel srvm = Mapper.Instance.Map<SimpleReviewViewModel>(review);
                srvm.ReviewerUsername = review.Reviewer.User.UserName;
                srvm.DaysAgoPublished = DateTime.Now.DayOfYear - review.PublishDate.DayOfYear;
                rovm.Reviews.Add(srvm);
            }

            return rovm;
        }
    }
}