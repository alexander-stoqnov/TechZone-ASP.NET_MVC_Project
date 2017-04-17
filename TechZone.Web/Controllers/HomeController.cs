namespace TechZone.Web.Controllers
{
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            string path = Server.MapPath("~/Scripts/CustomScripts/");
            var apikey = System.IO.File.ReadAllLines(path + "keys.txt");
            return View("Contact", null, apikey[0]);
        }
    }
}