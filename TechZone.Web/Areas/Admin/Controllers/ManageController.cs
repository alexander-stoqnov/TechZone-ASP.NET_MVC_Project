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

    [RouteArea("Admin")]
    [RoutePrefix("Manage")]
    [CustomAuthorize(Roles = "Admin")]
    public class ManageController : Controller
    {
        private AdminService service;

        public ManageController()
        {
            this.service = new AdminService();
        }

        [Route("Users")]
        public ActionResult Users()
        {
            UserRolesViewModel urvm = service.GetAllUsersAndRoles();
            return this.View(urvm);
        }

        [Route("Users")]
        [HttpPost]
        public ActionResult Users([Bind(Include = "Id,Roles")] ChangeUserRolesBindingModel curbm)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            service.ChangeUserRoles(curbm.Id);

            foreach (var role in curbm.Roles)
            {
                userManager.AddToRoles(curbm.Id, role);
            }
            return RedirectToAction("Users", "Manage", new { area = "Admin" });
        }
    }
}