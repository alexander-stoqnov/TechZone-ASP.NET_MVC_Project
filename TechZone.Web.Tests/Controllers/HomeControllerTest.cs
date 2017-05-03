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
        private readonly IProductsService _productsService;
        private readonly IReviewsService _reviewsService;
        private readonly IArticlesService _articlesService;
        private readonly HomeController _controller;

        public HomeControllerTest()
        {
            ConfigureMappings();
            this._productsService = new ProductsService();
            this._reviewsService = new ReviewsService();
            this._articlesService = new ArticlesService();
            this._controller = new HomeController(this._productsService, this._reviewsService, this._articlesService);
        }

        [TestMethod]
        public void HomeIndex_ShouldReturnItsDefaultView()
        {
            var result = _controller.Index() as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName));
        }

        [TestMethod]
        public void HomeIndex_ShouldReturnNotEmptyViewModel()
        {
            var result = _controller.Index() as ViewResult;
            var vm = result.Model as HomePageViewModel;
            Assert.IsNotNull(vm);
        }

        [TestMethod]
        public void HomeContact_ShouldReturItsDefaultView()
        {
            var result = _controller.Contact() as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName));
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
