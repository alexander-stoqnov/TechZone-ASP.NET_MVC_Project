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
    }
}