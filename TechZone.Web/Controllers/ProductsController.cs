namespace TechZone.Web.Controllers
{
    using System.Web.Mvc;
    using System.Collections.Generic;
    using System.Net.Http;
    using Models.ViewModels.Products;
    using System;
    using System.Linq;
    using PagedList;
    using Models.BindingModels;
    using Models.EntityModels;
    using Services.Contracts;

    [RoutePrefix("Products")]
    public class ProductsController : Controller
    {
        private readonly IProductsService _service;

        public ProductsController(IProductsService service)
        {
            this._service = service;
        }

        [Route("All")]
        public ActionResult All(int page = 1)
        {
            IEnumerable<GeneralProductPageViewModel> productVms = this._service.GetAllProducts();
            return View(productVms.ToPagedList(page, 12));
        }

        [Route("GraphicCards")]
        public ActionResult GraphicCards(int page = 1)
        {
            IEnumerable<GeneralProductPageViewModel> graphicCardVms = this._service.GetAllGraphicCards();
            return this.View("All", graphicCardVms.ToPagedList(page, 30));
        }

        [Route("HardDrives")]
        public ActionResult HardDrives(int page = 1)
        {
            IEnumerable<GeneralProductPageViewModel> hardDriveVms = this._service.GetAllHardDrives();
            return this.View("All", hardDriveVms.ToPagedList(page, 30));
        }

        [Route("Processors")]
        public ActionResult Processors(int page = 1)
        {
            IEnumerable<GeneralProductPageViewModel> processorVms = this._service.GetAllProcessors();
            return this.View("All", processorVms.ToPagedList(page, 30));
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

        [Route("FilterProducts")]
        public ActionResult FilterProducts(string priceRange, string orderType, string productName = "", int discount = 0)
        {
            var priceMinMax = GetPriceRangeMinMaxNumbers(priceRange);
            var client = new HttpClient();
            var response = client.GetAsync($"http://localhost:1575/api/products/all?$filter=substringof('{productName.ToLower()}', tolower(Name)) eq true and Price ge {priceMinMax[0]} and Price le {priceMinMax[1]} and Discount ge {discount}&$orderby={orderType}").Result;
            var products = response.Content.ReadAsAsync<IEnumerable<GeneralProductPageViewModel>>().Result;
            return this.PartialView("_GeneralProductViewPartial", products.ToPagedList(1, 30));
        }

        [Route("FilterHardDrives")]
        public ActionResult FilterHardDrives(AddHardDriveBindingModel ahbm, string priceRange, string orderType, string productName = "")
        {
            var priceMinMax = GetPriceRangeMinMaxNumbers(priceRange);
            var client = new HttpClient();
            var response = client.GetAsync($"http://localhost:1575/api/products/harddrives?driveBrand={ahbm.DriveBrand.ToString("G")}&driveType={ahbm.DriveType.ToString("G")}&$filter=substringof('{productName.ToLower()}', tolower(Name)) eq true and Price ge {priceMinMax[0]} and Price le {priceMinMax[1]} and Discount ge {ahbm.Discount} and Capacity ge {ahbm.Capacity}&$orderby={orderType}").Result;
            var products = response.Content.ReadAsAsync<IEnumerable<HardDrive>>().Result;
            return this.PartialView("_GeneralProductViewPartial", this._service.GetGeneralProductPageViewModels(products.ToList()).ToPagedList(1, 30));
        }

        [Route("FilterGraphicCards")]
        public ActionResult FilterGraphicCards(AddGraphicCardBindingModel agcbm, string priceRange, string orderType, string productName = "")
        {
            var priceMinMax = GetPriceRangeMinMaxNumbers(priceRange);
            var client = new HttpClient();
            var response = client.GetAsync($"http://localhost:1575/api/products/graphiccards?memoryType={agcbm.MemoryType.ToString("G")}&brand={agcbm.Brand.ToString("G")}&manufacturer={agcbm.Manufacturer.ToString("G")}&$filter=substringof('{productName.ToLower()}', tolower(Name)) eq true and Price ge {priceMinMax[0]} and Price le {priceMinMax[1]} and Discount ge {agcbm.Discount} and Memory ge {agcbm.Memory}&$orderby={orderType}").Result;
            var products = response.Content.ReadAsAsync<IEnumerable<GraphicCard>>().Result;
            return this.PartialView("_GeneralProductViewPartial", this._service.GetGeneralProductPageViewModels(products.ToList()).ToPagedList(1, 30));
        }

        [Route("FilterProcessors")]
        public ActionResult FilterProcessors(AddProcessorBindingModel apbm, string priceRange, string orderType, string productName = "")
        {
            var priceMinMax = GetPriceRangeMinMaxNumbers(priceRange);
            var client = new HttpClient();
            var response = client.GetAsync($"http://localhost:1575/api/products/processors?brand={apbm.Brand.ToString("G")}&series={apbm.Series.ToString("G")}&cores={apbm.Cores.ToString("G")}&$filter=substringof('{productName.ToLower()}', tolower(Name)) eq true and Price ge {priceMinMax[0]} and Price le {priceMinMax[1]} and Discount ge {apbm.Discount} and Cache ge {apbm.Cache} and ProcessorSpeed ge {apbm.ProcessorSpeed}&$orderby={orderType}").Result;
            var products = response.Content.ReadAsAsync<IEnumerable<Processor>>().Result;
            return this.PartialView("_GeneralProductViewPartial", this._service.GetGeneralProductPageViewModels(products.ToList()).ToPagedList(1, 30));
        }

        private int[] GetPriceRangeMinMaxNumbers(string priceRange)
        {
            return priceRange.Split(new[] { ' ', '$', '-' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        }
    }
}