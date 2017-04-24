using System.Collections.Generic;
using TechZone.Web.Attributes;

namespace TechZone.Web.Areas.Moderator.Controllers
{
    using System.Web.Mvc;
    using Services;
    using Models.ViewModels.Moderator;
    using Microsoft.AspNet.Identity;

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
            SubmitReportViewModel srvm = this._service.PrepareSubmitReportInfo(id);
            return View(srvm);
        }

        [Route("SubmitReport/{id}")]
        [Authorize(Roles = "Customer")]
        [HttpPost]
        public ActionResult SubmitReport(SubmitReportViewModel srbm)
        {
            var currentUserId = this.User.Identity.GetUserId();
            if (!ModelState.IsValid)
            {
                return RedirectToAction("SubmitReport", new {id = srbm.ReportedCommentId});
            }

            this._service.SendCommentReport(currentUserId, srbm);
            return RedirectToAction("Details", "Reviews", new { id = srbm.ReviewId });
        }

        [Route("EvaluateReports")]
        [CustomAuthorize(Roles = "Moderator")]
        public ActionResult EvaluateReports()
        {
            IEnumerable<EvaluateReportViewModel> reportsVms = this._service.GetAllUnevaluatedReports();
            return this.View(reportsVms);
        }

    }
}