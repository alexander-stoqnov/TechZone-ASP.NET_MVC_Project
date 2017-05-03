using System.Configuration;
using Microsoft.AspNet.Identity;

namespace TechZone.Web.Tests.Controllers
{
    using System.Web.Mvc;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Services;
    using Services.Contracts;
    using Web.Controllers;
    using AutoMapper;
    using Models.EntityModels;
    using Models.ViewModels.Home;

    [TestClass]
    public class HomeControllerTest
    {
        private IProductsService _productsService;
        private IReviewsService _reviewsService;
        private IArticlesService _articlesService;

        [TestInitialize]
        public void Init()
        {
            ConfigureMappings();
            this._productsService = new ProductsService();
            this._reviewsService = new ReviewsService();
            this._articlesService = new ArticlesService();
        }

        [TestMethod]
        public void HomeIndex_ShouldReturnItsDefaultView()
        {
            HomeController controller = new HomeController(this._productsService, this._reviewsService, this._articlesService);
            var result = controller.Index() as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName));
        }

        [TestMethod]
        public void HomeIndex_ShouldReturnNotEmptyViewModel()
        {
            HomeController controller = new HomeController(this._productsService, this._reviewsService, this._articlesService);
            var result = controller.Index() as ViewResult;
            var vm = result.Model as HomePageViewModel;
            Assert.IsNotNull(vm);
        }

        private void ConfigureMappings()
        {
            Mapper.Initialize(m =>
            {
                m.CreateMap<Product, LatestProductViewModel>();
                m.CreateMap<Review, LatestReviewViewModel>()
                .ForMember(lrvm => lrvm.ProductImage, expr => expr.MapFrom(r => r.Product.ImageUrl))
                .ForMember(lrvm => lrvm.ProductName, expr => expr.MapFrom(r => r.Product.Name))
                .ForMember(lrvm => lrvm.ProductId, expr => expr.MapFrom(r => r.Product.Id));
                m.CreateMap<Article, LatestArticleViewModel>();
            });
        }
    }
}
