﻿namespace TechZone.Web.Controllers
{
    using System.Web.Mvc;
    using Models.BindingModels;
    using Services;
    using Microsoft.AspNet.Identity;
    using Models.ViewModels.Reviews;

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
            var apikey = System.IO.File.ReadAllLines(Server.MapPath("~/Scripts/CustomScripts/") + "keys.txt");
            var currentUserId = this.User.Identity.GetUserId();
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Comment should be between 10 and 400 characters.");
                return this.View("Details", this._service.GetReviewDetails(currentUserId, acbm.Id, apikey[1]));
            }
            this._service.WriteCommentToReview(currentUserId, acbm);
            return RedirectToAction("Details", "Reviews", new { id = acbm.Id });
        }

        [HttpPost]
        [Route("Vote")]
        [Authorize]
        public ActionResult Vote(VoteForReviewViewModel vfrvm)
        {
            var currentUserId = this.User.Identity.GetUserId();
            //if (this._service.UserHasAlreadyVotedForReview(currentUserId, vfrvm.Id))
            //{
            //    return this.RedirectToAction("Details", "Reviews", new {id = vfrvm.Id});
            //}
            this._service.CastUserVote(currentUserId, vfrvm);
            return this.RedirectToAction("Details", "Reviews", new { id = vfrvm.Id });
        }
    }
}