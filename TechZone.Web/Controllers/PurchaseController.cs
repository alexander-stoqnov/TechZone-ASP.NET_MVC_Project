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

        public ActionResult AddToShoppingCart()
        {
            return View();
        }
    }
}