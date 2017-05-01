namespace TechZone.Web.Tests.Controllers
{
    using AutoMapper;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models.EntityModels;
    using Models.ViewModels.Products;
    using Services;
    using Services.Contracts;
    using System.Web.Mvc;
    using Web.Controllers;
    using System.Collections.Generic;
    using System.Linq;
    using TestStack.FluentMVCTesting;
    using Models.BindingModels;
    using Models.Enums;

    [TestClass]
    public class ProductsControllerTest
    {
        private IProductsService _productsService;
        private ProductsController _controller;

        [TestInitialize]
        public void Init()
        {
            ConfigureMappings();
            this._productsService = new ProductsService();
            this._controller = new ProductsController(this._productsService);
        }

        [TestMethod]
        public void ProductsAll_ShouldReturnItsDefaultView()
        {
            var result = _controller.All() as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName));
        }

        [TestMethod]
        public void ProductsAll_ShouldReturnProperViewModel()
        {
            var result = _controller.All() as ViewResult;
            var vm = result.Model as IEnumerable<GeneralProductPageViewModel>;
            Assert.IsNotNull(vm);
        }

        [TestMethod]
        public void ProductsAll_ThereShouldBeAtLeast30Products()
        {
            var result = _controller.All() as ViewResult;
            var vm = result.Model as IEnumerable<GeneralProductPageViewModel>;
            Assert.IsTrue(vm.Count() > 30);
        }

        [TestMethod]
        public void ProductsGraphicCards_ShouldReturnTheAllView()
        {
            var result = _controller.GraphicCards() as ViewResult;
            Assert.AreEqual("All", result.ViewName);
        }

        [TestMethod]
        public void ProductsHardDrives_ShouldReturnTheAllView()
        {
            var result = _controller.HardDrives() as ViewResult;
            Assert.AreEqual("All", result.ViewName);
        }

        [TestMethod]
        public void ProductsProcessors_ShouldReturnTheAllView()
        {
            var result = _controller.Processors() as ViewResult;
            Assert.AreEqual("All", result.ViewName);
        }

        [TestMethod]
        public void ProductsDetails_ShouldRedirectToAllIfInvalidId()
        {
            _controller.WithCallTo(pc => pc.Details(34325436))
                .ShouldRedirectTo(pc => pc.All);
        }

        [TestMethod]
        public void ProductsDetails_ShouldReturnProductDetailsWithValidId()
        {
            _controller.WithCallTo(pc => pc.Details(13))
                .ShouldRenderDefaultView();
        }

        [TestMethod]
        public void ProductsDetails_ShouldReturnProductDetailsProperModel()
        {
            var result = _controller.Details(13) as ViewResult;
            var vm = result.Model as ProductDetailsViewModel;
            Assert.IsNotNull(vm);
        }

        [TestMethod]
        public void ProductsHardWareSpecs_ShouldReturnAPartialView()
        {
            _controller.WithCallTo(pc => pc.HardwareSpecs(13))
          .ShouldRenderPartialView("_ProductSpecsPartial");
        }

        [TestMethod]
        public void ProductsFilter_ShouldRenderDefaultView()
        {
            _controller.WithCallTo(pc => pc.FilterProducts("$0 - $700", "Price desc", "gtx", 0))
          .ShouldRenderPartialView("_GeneralProductViewPartial");
        }

        [TestMethod]
        public void ProductsFilter_ShouldGiveProperlyFilteredProducts()
        {
            _controller.WithCallTo(c => c.FilterProducts("$0 - $300", "Price desc", "", 0))
                .ShouldRenderPartialView("_GeneralProductViewPartial")
                .WithModel<IEnumerable<GeneralProductPageViewModel>>(m => m.All(p => p.Price < 300));
        }

        [TestMethod]
        public void ProductsFilter_ShouldProperlyOrderFilteredProducts()
        {
            _controller.WithCallTo(c => c.FilterProducts("$0 - $500", "Price desc", "", 0))
                .ShouldRenderPartialView("_GeneralProductViewPartial")
                .WithModel<IEnumerable<GeneralProductPageViewModel>>(m => m.First().Price > m.Skip(1).First().Price);
        }

        [TestMethod]
        public void ProductsFilter_ShouldFilterDiscounts()
        {
            _controller.WithCallTo(c => c.FilterProducts("$0 - $700", "Price desc", "", 5))
                .ShouldRenderPartialView("_GeneralProductViewPartial")
                .WithModel<IEnumerable<GeneralProductPageViewModel>>(m => m.All(p => p.Discount >= 5));
        }

        [TestMethod]
        public void HardDrivesFilter_ShouldReturnProperHardDrives()
        {
            _controller.WithCallTo(c => c.FilterHardDrives(new AddHardDriveBindingModel()
            {
                Capacity = 80,
                DriveType = HardDriveType.SSD,
                DriveBrand = HardDriveBrandType.Samsung,
                Discount = 0
            }, "$0 - $700", "Price desc", ""))
                .ShouldRenderPartialView("_GeneralProductViewPartial")
                .WithModel<IEnumerable<GeneralProductPageViewModel>>(m => m.All(hd => hd.Name.Contains("Samsung")));
        }

        [TestMethod]
        public void GraphicCardsFilter_ShouldReturnProperGraphicCards()
        {
            _controller.WithCallTo(c => c.FilterGraphicCards(new AddGraphicCardBindingModel()
            {
                Brand = GraphicCardManufacturerType.Nvidia,
                MemoryType = GraphicCardMemoryType.GDDR5,
                Manufacturer = ManufacturerType.eVGA,
                Discount = 0
            }, "$0 - $700", "Price desc", ""))
                .ShouldRenderPartialView("_GeneralProductViewPartial")
                .WithModel<IEnumerable<GeneralProductPageViewModel>>(m => m.All(hd => hd.Name.ToLower().Contains("evga")));
        }

        [TestMethod]
        public void ProcessorsFilter_ShouldReturnProperProcessors()
        {
            _controller.WithCallTo(c => c.FilterProcessors(new AddProcessorBindingModel()
            {
                Brand = ProcessorBrandType.Intel,
                Cores = ProcessorCoresType.Quad_Core,
                Cache = 1,
                ProcessorSpeed = 2,
                Discount = 0
            }, "$0 - $700", "Price desc", ""))
                .ShouldRenderPartialView("_GeneralProductViewPartial")
                .WithModel<IEnumerable<GeneralProductPageViewModel>>(m => m.All(hd => hd.Name.ToLower().Contains("i7")));
        }

        private void ConfigureMappings()
        {
            Mapper.Initialize(m =>
            {
                m.CreateMap<Product, GeneralProductPageViewModel>();
                m.CreateMap<Product, ProductDetailsViewModel>();
            });
        }
    }
}
