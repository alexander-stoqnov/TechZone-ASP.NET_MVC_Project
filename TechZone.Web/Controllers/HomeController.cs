namespace TechZone.Web.Controllers
{
    using Models.ViewModels.Home;
    using System.Web.Mvc;
    using Services;

    public class HomeController : Controller
    {
        private ProductsService _productsService;
        private ReviewsService _reviewsService;

        public HomeController()
        {
            this._productsService = new ProductsService();
            this._reviewsService = new ReviewsService();
        }

        public ActionResult Index()
        {
            HomePageViewModel hpvm = new HomePageViewModel();
            hpvm.LatestProducts = this._productsService.GetHomePageLatestProducts();
            hpvm.LatestReviews = this._reviewsService.GetHomePageLatestReviews();
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