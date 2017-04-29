namespace TechZone.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Services.Contracts;
    using Microsoft.AspNet.Identity;
    using Models.ViewModels.Purchase;

    [RoutePrefix("Purchase")]
    public class PurchaseController : Controller
    {
        private readonly IPurchaseService _service;

        public PurchaseController(IPurchaseService service)
        {
            this._service = service;
        }

        [HttpPost]
        [Route("AddToShoppingCart")]
        public ActionResult AddToShoppingCart(int id)
        {
            if (!this._service.ProductExists(id))
            {
                return RedirectToAction("All", "Products");
            }

            if (!User.Identity.IsAuthenticated)
            {
                this._service.AddProductForCurrentSession(id, Session.SessionID);
            }
            else
            {
                this._service.AddProductToShoppingCart(id, User.Identity.GetUserId());
            }
            return RedirectToAction("All", "Products");
        }

        public ActionResult CountOfProductsInCart()
        {
            string currentUserId = this.User.Identity.GetUserId();
            int count = this._service.GetNumberOfItemsInCart(currentUserId, this.Session.SessionID);

            return this.PartialView("_NavigationHelpersPartial", count);
        }

        [Route("ShoppingCart")]
        public ActionResult ShoppingCart()
        {
            var userId = this.User.Identity.GetUserId();
            ShoppingCartViewModel cart = this._service.GetCartItems(userId, this.Session.SessionID);
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
            this._service.RemoveProductFromCart(currentUserId, this.Session.SessionID ,id);
            return RedirectToAction("ShoppingCart", "Purchase");
        }

        [Route("CheckOut")]
        [Authorize(Roles = "Customer")]
        public ActionResult CheckOut()
        {
            var currentUserId = this.User.Identity.GetUserId();

            if (this._service.ContainsItemsNotInStock(currentUserId))
            {
                return RedirectToAction("ShoppingCart", "Purchase");
            }

            FinalCheckoutViewModel finalItemsPrice = this._service.CalculatePriceWithoutShipment(currentUserId);
            return this.View(finalItemsPrice);
        }

        [Route("BalanceCheck")]
        [Authorize(Roles = "Customer")]
        public ActionResult BalanceCheck(decimal totalPrice)
        {
            var currentUserId = this.User.Identity.GetUserId();
            if (!this._service.EnoughCredits(currentUserId, totalPrice))
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
            var apikey = System.IO.File.ReadAllLines(Server.MapPath("~/Scripts/CustomScripts/") + "keys.txt");
            var currentUserId = this.User.Identity.GetUserId();
            if (!this._service.EnoughCredits(currentUserId, finalPrice))
            {
                return RedirectToAction("CheckOut", "Purchase");
            }
            this._service.FinalizePurchase(currentUserId, finalPrice, apikey[1]);
            return this.RedirectToAction("All", "Products");
        }
    }
}