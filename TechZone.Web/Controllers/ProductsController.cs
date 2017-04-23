namespace TechZone.Web.Controllers
{
    using System.Web.Mvc;
    using Services;
    using System.Collections.Generic;
    using Models.ViewModels.Products;

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

        [Route("Details/{id=1}")]
        public ActionResult Details(int id = 1)
        {
            if (!this.service.ProductExists(id))
            {
                return RedirectToAction("All");
            }

            ProductDetailsViewModel productDetailsVm = this.service.GetProductDetails(id);
            return this.View(productDetailsVm);
        }

        [Route("HardwareSpecs/{id}")]
        [ChildActionOnly]
        public ActionResult HardwareSpecs(int id)
        {
            Dictionary<string, string> specs = this.service.GetProductSpecs(id);
            return this.PartialView("_ProductSpecsPartial", specs);
        }
    }
}