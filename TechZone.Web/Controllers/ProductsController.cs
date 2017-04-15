using System.Collections.Generic;
using TechZone.Models.ViewModels.Products;

namespace TechZone.Web.Controllers
{
    using System.Web.Mvc;
    using Services;

    [RoutePrefix("Products")]
    public class ProductsController : Controller
    {
        private ProductsService service;

        public ProductsController()
        {
            this.service = new ProductsService();
        }

        [Route("All")]
        public ActionResult All()
        {
            IEnumerable<GeneralProductPageViewModel> productVms = this.service.GetAllProducts();
            return View(productVms);
        }
    }
}