namespace TechZone.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Services;
    using Microsoft.AspNet.Identity;
    using Models.ViewModels.Purchase;

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
            string currentUserId = this.User.Identity.GetUserId();
            int count = this.service.GetNumberOfItemsInCart(currentUserId, this.Session.SessionID);

            return this.PartialView("_NavigationHelpersPartial", count);
        }

        [Route("ShoppingCart")]
        public ActionResult ShoppingCart()
        {
            var userId = this.User.Identity.GetUserId();
            ShoppingCartViewModel cart = this.service.GetCartItems(userId, this.Session.SessionID);
            if (!cart.ProductsInCart.Any())
            {
                return RedirectToAction("All", "Products");
            }
            return this.View(cart);
        }

        [HttpPost]
        [Route("Delete")]
        public ActionResult RemoveFromCart(int id)
        {
            var currentUserId = this.User.Identity.GetUserId();
            this.service.RemoveProductFromCart(currentUserId, this.Session.SessionID ,id);
            return RedirectToAction("ShoppingCart", "Purchase");
        }

        [Route("CheckOut")]
        [Authorize(Roles = "Customer")]
        public ActionResult CheckOut()
        {
            var currentUserId = this.User.Identity.GetUserId();

            if (this.service.ContainsItemsNotInStock(currentUserId))
            {
                return RedirectToAction("ShoppingCart", "Purchase");
            }

            FinalCheckoutViewModel finalItemsPrice = this.service.CalculatePriceWithoutShipment(currentUserId);
            return this.View(finalItemsPrice);
        }

        [Route("BalanceCheck")]
        [Authorize(Roles = "Customer")]
        public ActionResult BalanceCheck(decimal totalPrice)
        {
            var currentUserId = this.User.Identity.GetUserId();
            if (!this.service.EnoughCredits(currentUserId, totalPrice))
            {
                return this.PartialView("_NotEnoughCreditsPartial");
            }
            return null;
        }

        [HttpPost]
        [Route("Finalize")]
        [Authorize(Roles = "Customer")]
        public ActionResult Finalize(decimal finalPrice)
        {
            var currentUserId = this.User.Identity.GetUserId();
            if (!this.service.EnoughCredits(currentUserId, finalPrice))
            {
                return RedirectToAction("CheckOut", "Purchase");
            }
            this.service.FinalizePurchase(currentUserId, finalPrice);
            return this.RedirectToAction("All", "Products");
        }
    }
}