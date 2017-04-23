namespace TechZone.Web.Areas.Moderator.Controllers
{
    using Attributes;
    using System.Web.Mvc;
    using Services;
    using Microsoft.AspNet.Identity;
    using Models.ViewModels.Moderator;

    [RouteArea("Moderator")]
    [RoutePrefix("Maintain")]
    public class MaintainController : Controller
    {
        private ModeratorService _service;

        public MaintainController()
        {
            this._service = new ModeratorService();
        }

        [Route("SubmitReport/{id}")]
        [Authorize(Roles = "Customer")]
        public ActionResult SubmitReport(int id)
        {
            var currentUserId = this.User.Identity.GetUserId();
            SubmitReportViewModel srvm = this._service.PrepareSubmitReportInfo(currentUserId, id);
            return View(srvm);
        }
    }
}