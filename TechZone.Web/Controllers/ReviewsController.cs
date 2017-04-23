namespace TechZone.Web.Controllers
{
    using System.Web.Mvc;
    using Models.BindingModels;
    using Services;
    using Microsoft.AspNet.Identity;
    using Models.ViewModels.Reviews;
    using System.Net;

    [RoutePrefix("Reviews")]
    public class ReviewsController : Controller
    {
        private ReviewsService _service;

        public ReviewsController()
        {
            this._service = new ReviewsService();
        }

        [ChildActionOnly]
        public ActionResult SubmitReview(int id)
        {
            var currentUserId = this.User.Identity.GetUserId();
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.PartialView("_SubmitReviewPartial", new SubmitReviewViewModel { Id = id } );
            }
            SubmitReviewViewModel srvm = this._service.CheckWhetherUserHasReviewedProduct(currentUserId, id);
            return this.PartialView("_SubmitReviewPartial", srvm);
        }

        [ChildActionOnly]
        public ActionResult LoadProductReviews(int id)
        {
            var apikey = System.IO.File.ReadAllLines(Server.MapPath("~/Scripts/CustomScripts/") + "keys.txt");
            ReviewOverviewViewModel rovm = this._service.GetReviewsForProduct(id, apikey[1]);
            return this.PartialView("_AllProductReviewPartial", rovm);
        }

        [HttpPost]
        [Route("Write")]
        [Authorize(Roles = "Customer")]
        public ActionResult Write(WriteReviewBindingModel wrbm)
        {
            if (!ModelState.IsValid || _service.HasUserReviewedProduct(User.Identity.GetUserId(), wrbm.ProductId))
            {
                return RedirectToAction("Details", "Products", new { id = wrbm.ProductId });
            }
            var currentUserId = this.User.Identity.GetUserId();
            this._service.CreateReview(currentUserId, wrbm);
            return RedirectToAction("Details", "Products", new { id = wrbm.ProductId });
        }

        [Route("Details/{id}")]
        public ActionResult Details(int id)
        {
            var apikey = System.IO.File.ReadAllLines(Server.MapPath("~/Scripts/CustomScripts/") + "keys.txt");
            if (!this._service.ReviewExists(id))
            {
                return this.RedirectToAction("All", "Products");
            }
            var currentUserId = this.User.Identity.GetUserId();
            ReviewDetailsViewModel rdvm = this._service.GetReviewDetails(currentUserId, id, apikey[1]);
            return this.View(rdvm);
        }

        [HttpPost]
        [Route("Comment")]
        [Authorize]
        public ActionResult Comment(AddCommentBindingModel acbm)
        {
            var currentUserId = this.User.Identity.GetUserId();
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Details", "Reviews", new { id = acbm.Id });
            }
            this._service.WriteCommentToReview(currentUserId, acbm);
            return RedirectToAction("Details", "Reviews", new { id = acbm.Id });
        }

        [Route("CommentForm")]
        [ChildActionOnly]
        public ActionResult CommentForm(int id)
        {
            AddCommentBindingModel acbm = new AddCommentBindingModel { Id = id};
            return this.PartialView("_WriteCommentPartial", acbm);
        }

        [HttpPost]
        [Route("Vote")]
        [Authorize]
        public ActionResult Vote(VoteForReviewViewModel vfrvm)
        {
            var currentUserId = this.User.Identity.GetUserId();
            if (this._service.UserHasAlreadyVotedForReview(currentUserId, vfrvm.Id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable);
            }
            this._service.CastUserVote(currentUserId, vfrvm);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}