using System.Linq;

namespace TechZone.Web.Controllers
{
    using Models.ViewModels.Home;
    using System.Web.Mvc;
    using Services;

    public class HomeController : Controller
    {
        private ProductsService _productsService;
        private ReviewsService _reviewsService;
        private ArticlesService _articlesService;

        public HomeController()
        {
            this._productsService = new ProductsService();
            this._reviewsService = new ReviewsService();
            this._articlesService = new ArticlesService();
        }

        public ActionResult Index()
        {
            var apikey = System.IO.File.ReadAllLines(Server.MapPath("~/Scripts/CustomScripts/") + "keys.txt");
            HomePageViewModel hpvm = new HomePageViewModel
            {
                LatestProducts = this._productsService.GetHomePageLatestProducts(),
                LatestReviews = this._reviewsService.GetHomePageLatestReviews(),
                LatestArticles = this._articlesService.GetHomePageLatestArticles(apikey[1]).ToList()
            };
            return View(hpvm);
        }

        public ActionResult Contact()
        {
            string path = Server.MapPath("~/Scripts/CustomScripts/");
            var apikey = System.IO.File.ReadAllLines(path + "keys.txt");
            return View("Contact", null, apikey[0]);
        }
    }
}