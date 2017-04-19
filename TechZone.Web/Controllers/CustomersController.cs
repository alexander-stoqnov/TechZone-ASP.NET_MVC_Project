namespace TechZone.Web.Controllers
{
    using System.Web.Mvc;

    [RoutePrefix("Customers")]
    public class CustomersController : Controller
    {
        [Route("Profile")]
        public ActionResult ProfilePage()
        {
            return View();
        }
    }
}