namespace TechZone.Web.Areas.Moderator.Controllers
{
    using System.Web.Mvc;
    using Services.Contracts;
    using Models.ViewModels.Moderator;
    using Microsoft.AspNet.Identity;
    using System.Collections.Generic;
    using System.Net;
    using Attributes;

    [RouteArea("Moderator")]
    [RoutePrefix("Maintain")]
    public class MaintainController : Controller
    {
        private readonly IModeratorService _service;

        public MaintainController(IModeratorService service)
        {
            this._service = service;
        }

        [Route("SubmitReport/{id?}")]
        [Authorize(Roles = "Customer")]
        public ActionResult SubmitReport(int? id)
        {
            if (id == null || !this._service.CommentExists(id))
            {
                return RedirectToAction("All", "Products", new {area = ""});
            }

            SubmitReportViewModel srvm = this._service.PrepareSubmitReportInfo(id);
            return View(srvm);
        }

        [Route("SubmitReport/{id}")]
        [Authorize(Roles = "Customer")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SubmitReport(SubmitReportViewModel srbm)
        {
            var currentUserId = this.User.Identity.GetUserId();
            if (!ModelState.IsValid)
            {
                return RedirectToAction("SubmitReport", new {id = srbm.ReportedCommentId});
            }

            this._service.SendCommentReport(currentUserId, srbm);
            return RedirectToAction("Details", "Reviews", new { id = srbm.ReviewId, area = "" });
        }

        [Route("EvaluateReports")]
        [CustomAuthorize(Roles = "Moderator")]
        public ActionResult EvaluateReports()
        {
            IEnumerable<EvaluateReportViewModel> reportsVms = this._service.GetAllUnevaluatedReports();
            return this.View(reportsVms);
        }

        [Route("DismissReport")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "Moderator")]
        public ActionResult DismissReport(int id)
        {
            if (!this._service.ReportStillExists(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            this._service.RemoveReport(id);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [Route("IssueWarning")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "Moderator")]
        public ActionResult IssueWarning(int id)
        {
            if (!this._service.ReportStillExists(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            this._service.IssueWarningToCustomer(id);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [Route("ChatRoom")]
        [Authorize(Roles = "Customer,Moderator")]
        public ActionResult ChatRoom(string roomId)
        {
            string currentUserId = User.Identity.GetUserId();
            if (!this._service.IsRoomForCurrentUser(currentUserId, roomId) && !this.User.IsInRole("Moderator"))
            {
                return RedirectToAction("All", "Products", new { area = ""});
            }
            ChatRoomUsernamesViewModel cruvm = new ChatRoomUsernamesViewModel
            {
                CurrentUserUsername = User.Identity.GetUserName(),
                RoomId = roomId
            };
            return this.View(cruvm);
        }

        [HttpPost]
        [Route("RemoveUserWarnings")]
        [CustomAuthorize(Roles = "Moderator")]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveUserWarnings(string roomId)
        {
            this._service.RemoveUserWarnings(roomId);
            return RedirectToAction("JudgeWarnings", "Maintain", new {area = "Moderator"});
        }

        [Route("JudgeWarnings")]
        [CustomAuthorize(Roles = "Moderator")]
        public ActionResult JudgeWarnings()
        {
            IEnumerable<UserWarningsViewModel> warningsVms = this._service.GetAllUserWarnings();
            return this.View(warningsVms);
        }
    }
}