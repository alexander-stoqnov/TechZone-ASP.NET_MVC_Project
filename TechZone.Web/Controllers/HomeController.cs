namespace TechZone.Web.Controllers
{
    using Models.ViewModels.Home;
    using System.Web.Mvc;
    using System.Linq;
    using Services.Contracts;

    public class HomeController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly IReviewsService _reviewsService;
        private readonly IArticlesService _articlesService;

        public HomeController(IProductsService productsService, IReviewsService reviewsService, IArticlesService articlesService)
        {
            this._productsService = productsService;
            this._reviewsService = reviewsService;
            this._articlesService = articlesService;
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