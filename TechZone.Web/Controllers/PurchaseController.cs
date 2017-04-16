using Microsoft.AspNet.Identity;

namespace TechZone.Web.Controllers
{
    using System.Web.Mvc;
    using Services;

    public class PurchaseController : Controller
    {
        private PurchaseService service;

        public PurchaseController()
        {
            this.service = new PurchaseService();
        }

        [HttpPost]
        public ActionResult AddToShoppingCart(int id)
        {
            if (!this.service.ProductExists(id))
            {
                return RedirectToAction("All", "Products");
            }

            if (!User.Identity.IsAuthenticated)
            {
                this.service.AddProductForCurrentSession(id, Session.SessionID);
            }
            else
            {
                this.service.AddProductToShoppingCart(id, User.Identity.GetUserId());
            }
            return RedirectToAction("All", "Products");
        }

        [ChildActionOnly]
        public ActionResult CountOfProductsInCart()
        {
            int count = 0;
            if (User.Identity.IsAuthenticated)
            {
                string currentUserId = this.User.Identity.GetUserId();
                count = this.service.ProductsInCartForLoggedInUser(currentUserId);
            }
            else
            {
                count = this.service.ProductsInCartForGuest(this.Session.SessionID);
            }
            return this.PartialView("_NavigationHelpersPartial", count);
        }
    }
}