using AutoMapper;
using TechZone.Models.EntityModels;

namespace TechZone.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using Attributes;
    using Services;
    using System.Web;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Models.BindingModels;
    using Models.ViewModels.Admin;
    using System.Collections.Generic;

    [RouteArea("Admin")]
    [RoutePrefix("Control")]
    [CustomAuthorize(Roles = "Admin")]
    public class ControlController : Controller
    {
        private readonly AdminService _service;

        public ControlController()
        {
            this._service = new AdminService();
        }

        [Route("Users")]
        public ActionResult Users()
        {
            UserRolesViewModel urvm = _service.GetAllUsersAndRoles();
            return this.View(urvm);
        }

        [Route("Users")]
        [HttpPost]
        public ActionResult Users([Bind(Include = "Id,Roles")] ChangeUserRolesBindingModel curbm)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            _service.ChangeUserRoles(curbm.Id);

            foreach (var role in curbm.Roles)
            {
                userManager.AddToRoles(curbm.Id, role);
            }
            return RedirectToAction("Users", "Control", new { area = "Admin" });
        }

        [Route("Products")]
        public ActionResult Products()
        {
            IEnumerable<ManageProductViewModel> manageProductVms = this._service.GetProductsToManage();
            return this.View(manageProductVms);
        }

        [HttpPost]
        [Route("DeleteProduct")]
        public ActionResult ToggleProductAvailability(int id)
        {
            if (!this._service.ProductExists(id))
            {
                return RedirectToAction("Products", "Control");
            }
            this._service.DisableEnableSelectedProduct(id);
            return RedirectToAction("Products", "Control");
        }

        [Route("EditProduct")]
        public ActionResult EditProduct(int id)
        {
            if (!this._service.ProductExists(id))
            {
                return RedirectToAction("Products", "Control");
            }
            EditProductViewModel epvm = this._service.GetProductToEditDetails(id);
            return this.View(epvm);
        }

        [Route("EditProduct")]
        [HttpPost]
        public ActionResult EditProduct(EditProductViewModel epbm)
        {
            if (!ModelState.IsValid)
            {
                return this.View("EditProduct", epbm);
            }
            this._service.EditProductInfo(epbm);
            return RedirectToAction("Products");
        }

        [Route("AddProduct")]
        public ActionResult AddProduct()
        {
            return this.View();
        }

        [Route("ProductSpecs")]
        public ActionResult ProductSpecs(string productType)
        {
            if (productType.ToLower() == "harddrive")
            {
                return this.PartialView("_AddNewHardDrivePartial");
            }
            if (productType.ToLower() == "graphiccard")
            {
                return this.PartialView("_AddNewGraphicCardPartial");
            }
            return null;
        }

        [HttpPost]
        [Route("AddHardDrive")]
        public ActionResult AddHardDrive(AddHardDriveBindingModel ahdbm)
        {
            HardDrive hardDrive = Mapper.Instance.Map<HardDrive>(ahdbm);
            return null;
        }

        [HttpPost]
        [Route("AddGraphicCard")]
        public ActionResult AddGraphicCard(AddGraphicCardBindingModel agcbm)
        {
            GraphicCard graphicCard = Mapper.Instance.Map<GraphicCard>(agcbm);
            return null;
        }
    }
}