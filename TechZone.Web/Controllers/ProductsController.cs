using System;
using System.Linq;
using PagedList;

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
        public ActionResult All(int page = 1)
        {
            IEnumerable<GeneralProductPageViewModel> productVms = this._service.GetAllProducts();
            return View(productVms.ToPagedList(page, 12));
        }

        [Route("GraphicCards")]
        public ActionResult GraphicCards(int page = 1)
        {
            IEnumerable<GeneralProductPageViewModel> graphicCardVms = this._service.GetAllGraphicCards();
            return this.View("All", graphicCardVms.ToPagedList(page, 12));
        }

        [Route("HardDrives")]
        public ActionResult HardDrives(int page = 1)
        {
            IEnumerable<GeneralProductPageViewModel> hardDriveVms = this._service.GetAllHardDrives();
            return this.View("All", hardDriveVms.ToPagedList(page, 12));
        }

        [Route("Processors")]
        public ActionResult Processors(int page = 1)
        {
            IEnumerable<GeneralProductPageViewModel> processorVms = this._service.GetAllProcessors();
            return this.View("All", processorVms.ToPagedList(page, 12));
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

        [Route("TestApi")]
        public ActionResult FilterProducts(string priceRange, string productName = "", int discount = 0)
        {
            var priceMinMax = priceRange.Split(new[] {' ', '$', '-'}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            int minPrice = priceMinMax[0];
            int maxPrice = priceMinMax[1];
            var client = new HttpClient();
            var response = client.GetAsync($"http://localhost:1575/api/products/all?$filter=substringof('{productName.ToLower()}', tolower(Name)) eq true and Price ge {minPrice} and Price le {maxPrice} and Discount ge {discount}").Result;
            var products = response.Content.ReadAsAsync<IEnumerable<GeneralProductPageViewModel>>().Result;
            return View("All", products.ToPagedList(1, 12));
        }
    }
}