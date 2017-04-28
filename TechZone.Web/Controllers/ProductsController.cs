namespace TechZone.Web.Controllers
{
    using System.Web.Mvc;
    using Services;
    using System.Collections.Generic;
    using System.Net.Http;
    using Models.ViewModels.Products;

    [RoutePrefix("Products")]
    public class ProductsController : Controller
    {
        private ProductsService _service;

        public ProductsController()
        {
            this._service = new ProductsService();
        }

        [Route("All")]
        public ActionResult All()
        {
            IEnumerable<GeneralProductPageViewModel> productVms = this._service.GetAllProducts();
            return View(productVms);
        }

        [Route("GraphicCards")]
        public ActionResult GraphicCards()
        {
            IEnumerable<GeneralProductPageViewModel> graphicCardVms = this._service.GetAllGraphicCards();
            return this.View("All", graphicCardVms);
        }

        [Route("HardDrives")]
        public ActionResult HardDrives()
        {
            IEnumerable<GeneralProductPageViewModel> hardDriveVms = this._service.GetAllHardDrives();
            return this.View("All", hardDriveVms);
        }

        [Route("Processors")]
        public ActionResult Processors()
        {
            IEnumerable<GeneralProductPageViewModel> processorVms = this._service.GetAllProcessors();
            return this.View("All", processorVms);
        }

        [Route("Details/{id=1}")]
        public ActionResult Details(int id = 1)
        {
            if (!this._service.ProductExists(id))
            {
                return RedirectToAction("All");
            }

            ProductDetailsViewModel productDetailsVm = this._service.GetProductDetails(id);
            return this.View(productDetailsVm);
        }

        [Route("HardwareSpecs/{id}")]
        [ChildActionOnly]
        public ActionResult HardwareSpecs(int id)
        {
            Dictionary<string, string> specs = this._service.GetProductSpecs(id);
            return this.PartialView("_ProductSpecsPartial", specs);
        }

        [Route("TestApi/{productName}")]
        public ActionResult TestApi(string productName)
        {
            var client = new HttpClient();
            var response = client.GetAsync($"http://localhost:1575/api/products/all?$filter=substringof('{productName.ToLower()}', tolower(Name)) eq true").Result;
            var products = response.Content.ReadAsAsync<IEnumerable<GeneralProductPageViewModel>>().Result;
            return View("All", products);
        }
    }
}