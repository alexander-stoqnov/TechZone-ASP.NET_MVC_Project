namespace TechZone.Web.Controllers
{
    using System.Web.Mvc;
    using System.Collections.Generic;
    using System.Net.Http;
    using Models.ViewModels.Products;
    using System;
    using System.Linq;
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
        //[OutputCache(Duration = 30 * 60)]
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
        [HandleError(ExceptionType = typeof(InvalidOperationException), View = "NotFound")]
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
            var response = client.GetAsync($"http://localhost:15778/api/products/all?$filter=substringof('{productName.ToLower()}', tolower(Name)) eq true and Price ge {priceMinMax[0]} and Price le {priceMinMax[1]} and Discount ge {discount}&$orderby={orderType}").Result;
            var products = response.Content.ReadAsAsync<IEnumerable<GeneralProductPageViewModel>>().Result;
            return this.PartialView("_GeneralProductViewPartial", products);
        }

        [Route("FilterHardDrives")]
        public ActionResult FilterHardDrives(AddHardDriveBindingModel ahbm, string priceRange, string orderType, string productName = "")
        {
            var priceMinMax = GetPriceRangeMinMaxNumbers(priceRange);
            var client = new HttpClient();
            var response = client.GetAsync($"http://localhost:15778/api/products/harddrives?driveBrand={ahbm.DriveBrand.ToString("G")}&driveType={ahbm.DriveType.ToString("G")}&$filter=substringof('{productName.ToLower()}', tolower(Name)) eq true and Price ge {priceMinMax[0]} and Price le {priceMinMax[1]} and Discount ge {ahbm.Discount} and Capacity ge {ahbm.Capacity}&$orderby={orderType}").Result;
            var products = response.Content.ReadAsAsync<IEnumerable<HardDrive>>().Result;
            return this.PartialView("_GeneralProductViewPartial", this._service.GetGeneralProductPageViewModels(products.ToList()));
        }

        [Route("FilterGraphicCards")]
        public ActionResult FilterGraphicCards(AddGraphicCardBindingModel agcbm, string priceRange, string orderType, string productName = "")
        {
            var priceMinMax = GetPriceRangeMinMaxNumbers(priceRange);
            var client = new HttpClient();
            var response = client.GetAsync($"http://localhost:15778/api/products/graphiccards?memoryType={agcbm.MemoryType.ToString("G")}&brand={agcbm.Brand.ToString("G")}&manufacturer={agcbm.Manufacturer.ToString("G")}&$filter=substringof('{productName.ToLower()}', tolower(Name)) eq true and Price ge {priceMinMax[0]} and Price le {priceMinMax[1]} and Discount ge {agcbm.Discount} and Memory ge {agcbm.Memory}&$orderby={orderType}").Result;
            var products = response.Content.ReadAsAsync<IEnumerable<GraphicCard>>().Result;
            return this.PartialView("_GeneralProductViewPartial", this._service.GetGeneralProductPageViewModels(products.ToList()));
        }

        [Route("FilterProcessors")]
        public ActionResult FilterProcessors(AddProcessorBindingModel apbm, string priceRange, string orderType, string productName = "")
        {
            var priceMinMax = GetPriceRangeMinMaxNumbers(priceRange);
            var client = new HttpClient();
            var response = client.GetAsync($"http://localhost:15778/api/products/processors?brand={apbm.Brand.ToString("G")}&series={apbm.Series.ToString("G")}&cores={apbm.Cores.ToString("G")}&$filter=substringof('{productName.ToLower()}', tolower(Name)) eq true and Price ge {priceMinMax[0]} and Price le {priceMinMax[1]} and Discount ge {apbm.Discount} and Cache ge {apbm.Cache} and ProcessorSpeed ge {apbm.ProcessorSpeed}&$orderby={orderType}").Result;
            var products = response.Content.ReadAsAsync<IEnumerable<Processor>>().Result;
            return this.PartialView("_GeneralProductViewPartial", this._service.GetGeneralProductPageViewModels(products.ToList()));
        }

        private int[] GetPriceRangeMinMaxNumbers(string priceRange)
        {
            return priceRange.Split(new[] { ' ', '$', '-' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        }
    }
}