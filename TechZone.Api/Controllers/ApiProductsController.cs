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
        public IHttpActionResult getFilteredProducts()
        {
            return Ok(this._service.GetAllProducts());
        }
    }
}