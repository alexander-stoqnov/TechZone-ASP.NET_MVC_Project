namespace TechZone.Web.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models.EntityModels;
    using Models.ViewModels.Admin;
    using Services;
    using Services.Contracts;
    using Areas.Admin.Controllers;
    using TestStack.FluentMVCTesting;

    /// <summary>
    /// These tests are for the functionality of the admin controller
    /// </summary>
    [TestClass]
    public class AdminControllerTest
    {
        private readonly IAdminService _service;
        private readonly ControlController _controller;

        public AdminControllerTest()
        {
            ConfigureMappings();
            this._service = new AdminService();
            this._controller = new ControlController(this._service);
        }

        [TestMethod]
        public void ControlUsers_ShouldRenderItsDefaultView()
        {
            _controller.WithCallTo(pc => pc.Users())
                 .ShouldRenderDefaultView();
        }

        [TestMethod]
        public void ControlUsers_ShouldUseItsProperViewModel()
        {
            var result = _controller.Users() as ViewResult;
            var vm = result.Model as UserRolesViewModel;
            Assert.IsNotNull(vm);
        }

        [TestMethod]
        public void ControlUsers_ShouldHaveAllFourRoles()
        {
            _controller.WithCallTo(pc => pc.Users())
                .ShouldRenderDefaultView()
                .WithModel<UserRolesViewModel>(m => m.Roles.Count() == 4);
        }

        [TestMethod]
        public void ControlUsers_ShouldHaveAtLeastOneRegisteredUserAtAllTimes()
        {
            _controller.WithCallTo(pc => pc.Users())
                .ShouldRenderDefaultView()
                .WithModel<UserRolesViewModel>(m => m.Users.Any());
        }

        [TestMethod]
        public void ControlProducts_ShouldCallItsDefaultView()
        {
            _controller.WithCallTo(pc => pc.Products())
                 .ShouldRenderDefaultView();
        }

        [TestMethod]
        public void ControlProducts_ShouldUseProperViewModel()
        {
            var result = _controller.Products() as ViewResult;
            var vm = result.Model as IEnumerable<ManageProductViewModel>;
            Assert.IsNotNull(vm);
        }

        [TestMethod]
        public void ControlProducts_ShouldHoldAtLeast10ProductsAtAllTimes()
        {
            _controller.WithCallTo(pc => pc.Products())
                .ShouldRenderDefaultView()
                .WithModel<IEnumerable<ManageProductViewModel>>(m => m.Count() >= 10);
        }

        [TestMethod]
        public void ControlDisableProduct_ShouldRedirectIfInvalidIdIsEntered()
        {
            _controller.WithCallTo(pc => pc.ToggleProductAvailability(2000000))
                .ShouldRedirectTo(ac => ac.Products);
        }

        [TestMethod]
        public void ControlDisableProduct_ShouldReturnPartialViewForValidId()
        {
            _controller.WithCallTo(pc => pc.ToggleProductAvailability(6))
                .ShouldRenderPartialView("~/Areas/Admin/Views/Shared/_ProductAvailabilityButtonPartial.cshtml");
        }

        [TestMethod]
        public void ControlEditProduct_ShouldRedirectIfInvalidIdIsEntered()
        {
            _controller.WithCallTo(pc => pc.ToggleProductAvailability(2000000))
                .ShouldRedirectTo(ac => ac.Products);
        }

        [TestMethod]
        public void ControlEditProduct_ShouldReturnProperViewModel()
        {
            var result = _controller.EditProduct(5) as ViewResult;
            var vm = result.Model as EditProductViewModel;
            Assert.IsNotNull(vm);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException))]
        public void ControlEditProduct_ShouldThrowErrorIfModelStateIsInvalid()
        {
            EditProductViewModel epvm = new EditProductViewModel
            {
                Id = 3,
                Description = "meh",
                Discount = -5,
                Name = "ds",
                Price = -15,
                Quantity = -30
            };
            _controller.WithCallTo(c => c.EditProduct(epvm))
                .ShouldRenderDefaultView();
        }

        [TestMethod]
        public void ControlAddProduct_ShouldRenderItsDefaultView()
        {
            _controller.WithCallTo(pc => pc.AddProduct())
                 .ShouldRenderDefaultView();
        }

        [TestMethod]
        public void ControlAddProduct_ShouldRenderProperPartialForHardDrive()
        {
            _controller.WithCallTo(pc => pc.ProductSpecs("hArDDriVe"))
                .ShouldRenderPartialView("_AddNewHardDrivePartial");
        }

        [TestMethod]
        public void ControlAddProduct_ShouldRenderProperPartialForGraphicCard()
        {
            _controller.WithCallTo(pc => pc.ProductSpecs("GRAPHICCARD"))
                .ShouldRenderPartialView("_AddNewGraphicCardPartial");
        }

        [TestMethod]
        public void ControlAddProduct_ShouldRenderProperPartialForProcessor()
        {
            _controller.WithCallTo(pc => pc.ProductSpecs("processor"))
                .ShouldRenderPartialView("_AddNewProcessorPartial");
        }

        [TestMethod]
        public void ControlAddProduct_ShouldRenderNothingIfStringIsNotValid()
        {
            _controller.WithCallTo(pc => pc.ProductSpecs("invalidProductType"))
                .ShouldReturnEmptyResult();
        }

        private void ConfigureMappings()
        {
            Mapper.Initialize(m =>
            {
                m.CreateMap<IdentityRole, RoleViewModel>();
                m.CreateMap<Product, ManageProductViewModel>();
                m.CreateMap<Product, EditProductViewModel>();
            });
        }
    }
}
