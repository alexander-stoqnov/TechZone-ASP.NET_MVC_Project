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