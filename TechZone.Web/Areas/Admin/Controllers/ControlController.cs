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
    }
}