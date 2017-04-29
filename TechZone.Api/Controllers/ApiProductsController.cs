namespace TechZone.Api.Controllers
{
    using System.Web.Http;
    using Services;
    using System.Web.Http.OData;

    [RoutePrefix("Api/Products")]
    public class ApiProductsController : ApiController
    {
        private ProductsService _service;

        public ApiProductsController()
        {
            this._service = new ProductsService();
        }

        [Route("All")]
        [EnableQuery]
        public IHttpActionResult GetFilteredProducts()
        {
            return Ok(this._service.GetAllProducts());
        }

        [Route("HardDrives")]
        [EnableQuery]
        public IHttpActionResult GetFilteredHardDrives(string driveBrand, string driveType)
        {
            return Ok(this._service.GetHardDrivesForApi(driveBrand, driveType));
        }

        [Route("GraphicCards")]
        [EnableQuery]
        public IHttpActionResult GetFilteredGraphicCards(string memoryType, string brand, string manufacturer)
        {
            return Ok(this._service.GetGraphicCardsForApi(memoryType, brand, manufacturer));
        }

        [Route("Processors")]
        [EnableQuery]
        public IHttpActionResult GetFilteredProcessors(string brand, string series, string cores)
        {
            return Ok(this._service.GetProcessorsForApi(brand, series, cores));
        }
    }
}