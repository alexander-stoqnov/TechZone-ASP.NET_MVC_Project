namespace TechZone.Api.Controllers
{
    using System.Web.Http;
    using Services;
    using System.Web.Http.OData;
    using System.Linq;

    [RoutePrefix("Api/Products")]
    public class ApiProductsController : ApiController
    {
        private readonly ProductsService _service;

        public ApiProductsController()
        {
            this._service = new ProductsService();
        }

        [Route("All")]
        [EnableQuery]
        public IHttpActionResult GetFilteredProducts()
        {
            return Ok(this._service.GetAllProducts().AsEnumerable());
        }

        [Route("HardDrives")]
        [EnableQuery]
        public IHttpActionResult GetFilteredHardDrives(string driveBrand, string driveType)
        {
            string[] validHardDriveBrands = {"WesternDigital", "Seagate", "Toshiba", "Samsung", "Kingston", "SanDisk"};
            if (!validHardDriveBrands.Any(hdb => hdb.Equals(driveBrand)))
            {
                return BadRequest("You have selected an invalid hard drive brand");
            }
            if (driveType != "SSD" && driveType != "HDD")
            {
                return BadRequest("You have selected an invalid hard drive type");
            }
            return Ok(this._service.GetHardDrivesForApi(driveBrand, driveType).AsEnumerable());
        }

        [Route("GraphicCards")]
        [EnableQuery]
        public IHttpActionResult GetFilteredGraphicCards(string memoryType, string brand, string manufacturer)
        {
            string[] validManufacturers = {"Gigabyte", "ASUS", "eVGA", "MSI", "Palit"};
            if (memoryType != "DDR3" && memoryType != "GDDR5")
            {
                return BadRequest("You have selected an invalid graphic card memory type");
            }
            if (brand != "Nvidia" && brand != "Amd")
            {
                return BadRequest("You have selected an invalid graphic card manufacturer brand");
            }
            if (!validManufacturers.Any(m => m.Equals(manufacturer)))
            {
                return BadRequest("You have selected an invalid manufacturer");
            }

            return Ok(this._service.GetGraphicCardsForApi(memoryType, brand, manufacturer).AsEnumerable());
        }

        [Route("Processors")]
        [EnableQuery]
        public IHttpActionResult GetFilteredProcessors(string brand, string series, string cores)
        {
            string[] validCpuSeries = {"i3", "i5", "i7", "FX", "A", "Ryzen"};
            if (brand != "Intel" && brand != "AMD")
            {
                return BadRequest("You have selected an invalid cpu brand");
            }
            if (!validCpuSeries.Any(s => s.Equals(series)))
            {
                return BadRequest("You have selected an invalid cpu serie");
            }
            if (cores != "Dual_Core" && cores != "Quad_Core" && cores != "Octa_Core")
            {
                return BadRequest("You have selected invalic Cpu core type");
            }
            return Ok(this._service.GetProcessorsForApi(brand, series, cores).AsEnumerable());
        }
    }
}