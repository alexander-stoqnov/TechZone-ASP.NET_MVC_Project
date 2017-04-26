namespace TechZone.Web.Controllers
{
    using Models.ViewModels.Home;
    using System.Web.Mvc;
    using Services;

    public class HomeController : Controller
    {
        private ProductsService _productsService;

        public HomeController()
        {
            this._productsService = new ProductsService();
        }

        public ActionResult Index()
        {
            HomePageViewModel hpvm = this._productsService.GetHomePageInfo();
            return View(hpvm);
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