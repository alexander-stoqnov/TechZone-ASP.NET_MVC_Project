using TechZone.Models.ViewModels.Reviews;

namespace TechZone.Web.Controllers
{
    using System.Web.Mvc;
    using Models.BindingModels;
    using Services;
    using Microsoft.AspNet.Identity;

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
            return this.PartialView("_SubmitReviewPartial", id);
        }

        [ChildActionOnly]
        public ActionResult LoadProductReviews(int id)
        {
            ReviewOverviewViewModel rovm = this._service.GetReviewsForProduct(id);
            return this.PartialView("_AllProductReviewPartial", rovm);
        }

        [HttpPost]
        [Route("Write")]
        [Authorize(Roles = "Customer")]
        public ActionResult Write(WriteReviewBindingModel wrbm)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Details", "Products", new { id = wrbm.ProductId });
            }
            var currentUserId = this.User.Identity.GetUserId();
            this._service.CreateReview(currentUserId, wrbm);
            return RedirectToAction("Details", "Products", new { id = wrbm.ProductId });
        }
    }
}