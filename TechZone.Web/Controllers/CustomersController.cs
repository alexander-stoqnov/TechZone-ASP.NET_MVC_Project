namespace TechZone.Web.Controllers
{
    using System.Web.Mvc;
    using Services;
    using System.Collections.Generic;
    using Models.ViewModels.Customer;
    using Microsoft.AspNet.Identity;
    using Attributes;


    [RoutePrefix("Customers")]
    [CustomAuthorize(Roles = "Customer")]
    public class CustomersController : Controller
    {
        private readonly CustomersService _service;

        public CustomersController()
        {
            this._service = new CustomersService();
        }

        [Route("UserProfile")]
        public ActionResult UserProfile()
        {
            var currentUserId = this.User.Identity.GetUserId();
            IEnumerable<CustomerPurchaseHistoryViewModel> purchaseHistory = this._service.GetCurrentUserPurchases(currentUserId);
            return View(purchaseHistory);
        }
    }
}