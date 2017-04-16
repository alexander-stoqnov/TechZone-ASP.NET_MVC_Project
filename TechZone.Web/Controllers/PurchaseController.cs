namespace TechZone.Web.Controllers
{
    using System.Web.Mvc;
    using Services;
    using Microsoft.AspNet.Identity;

    [RoutePrefix("Purchase")]
    public class PurchaseController : Controller
    {
        private PurchaseService service;

        public PurchaseController()
        {
            this.service = new PurchaseService();
        }

        [HttpPost]
        [Route("AddToShoppingCart")]
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
            string currentUserId = this.User.Identity.GetUserId();
            count = this.service.GetNumberOfItemsInCart(currentUserId, this.Session.SessionID);

            return this.PartialView("_NavigationHelpersPartial", count);
        }

        [Route("ShoppingCart")]
        public ActionResult ShoppingCart()
        {
            var userId = this.User.Identity.GetUserId();
            // ShoppingCartViewModel cart = this.
            return null;
        }
    }
}