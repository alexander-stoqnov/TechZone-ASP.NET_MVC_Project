namespace TechZone.Services
{
    using System.Collections.Generic;
    using Models.ViewModels.Products;
    using System.Linq;
    using AutoMapper;
    using Models.ViewModels.Home;
    using System;
    using Models.EntityModels;
    using Models.Enums;

    public class ProductsService : Service
    {
        public IEnumerable<GeneralProductPageViewModel> GetAllProducts()
        {
            var products = this.Context.Products.OrderByDescending(p => p.Views).ToList();
            return GetGeneralProductPageViewModels(products);
        }

        public IEnumerable<GeneralProductPageViewModel> GetAllGraphicCards()
        {
            var graphicCards = this.Context.GraphicCards.OrderByDescending(gc => gc.Views).ToList();
            return this.GetGeneralProductPageViewModels(graphicCards);
        }

        public IEnumerable<GeneralProductPageViewModel> GetAllHardDrives()
        {
            var hardDrives = this.Context.HardDrives.OrderByDescending(gc => gc.Views).ToList();
            return this.GetGeneralProductPageViewModels(hardDrives);
        }

        public IEnumerable<GeneralProductPageViewModel> GetAllProcessors()
        {
            var processors = this.Context.Processors.OrderByDescending(gc => gc.Views).ToList();
            return this.GetGeneralProductPageViewModels(processors);
        }

        public HashSet<GeneralProductPageViewModel> GetGeneralProductPageViewModels<T>(List<T> products) where T : Product
        {
            var productVms = new HashSet<GeneralProductPageViewModel>();
            foreach (var product in products)
            {
                var productVm = Mapper.Map<GeneralProductPageViewModel>(product);
                if (productVm.Name.Length > 35)
                {
                    productVm.Name = productVm.Name.Substring(0, 35) + "...";
                }
                productVm.FinalPrice = CalculateFinalPrice(product.Discount, product.Price);
                if (product.IsAvailable)
                {
                    productVms.Add(productVm);
                }
            }
            return productVms;
        }

        public bool ProductExists(int id)
        {
            return this.Context.Products.Find(id) != null;
        }

        public ProductDetailsViewModel GetProductDetails(int id)
        {
            var product = this.Context.Products.Find(id);
            product.Views++;
            var productDetailsVm = Mapper.Map<ProductDetailsViewModel>(product);
            if (product.Discount != 0)
            {
                productDetailsVm.FinalPrice = CalculateFinalPrice(product.Discount, product.Price);
            }
            this.Context.SaveChanges();
            return productDetailsVm;
        }

        private decimal CalculateFinalPrice(int discount, decimal price)
        {
            decimal discountFinal = discount / 100.0m;
            return price - price * discountFinal;
        }

        public Dictionary<string, string> GetProductSpecs(int id)
        {
            Dictionary<string, string> specs = new Dictionary<string, string>();

            if (ProductIsGraphicCard(id))
            {
                var graphicCard = this.Context.GraphicCards.Find(id);
                specs["Brand"] = graphicCard.Brand.ToString("G");
                specs["Manufacturer"] = graphicCard.Manufacturer.ToString("G");
                specs["Memory Type"] = graphicCard.MemoryType.ToString("G");
                specs["Memory Size"] = graphicCard.Memory + " Gb";
                return specs;
            }

            if (ProductIsHardDrive(id))
            {
                var hardDrive = this.Context.HardDrives.Find(id);
                specs["Brand"] = hardDrive.DriveBrand.ToString("G");
                specs["Type"] = hardDrive.DriveType.ToString("G");
                specs["Capacity (Gb)"] = hardDrive.Capacity.ToString("G") + " Gb";
            }

            if (ProductIsProcessor(id))
            {
                var processor = this.Context.Processors.Find(id);
                specs["Brand"] = processor.Brand.ToString("G");
                specs["Series"] = processor.Series.ToString("G");
                specs["Cores"] = processor.Cores.ToString("G").Replace("_", " ");
                specs["Clock Speed"] = processor.ProcessorSpeed.ToString("0.00") + " Ghz";
                specs["Cache"] = processor.Cache + " Mb";
            }
            return specs;
        }

        private bool ProductIsGraphicCard(int id)
        {
            return this.Context.GraphicCards.Any(g => g.Id == id);
        }

        private bool ProductIsHardDrive(int id)
        {
            return this.Context.HardDrives.Any(hd => hd.Id == id);
        }

        private bool ProductIsProcessor(int id)
        {
            return this.Context.Processors.Any(pr => pr.Id == id);
        }

        public ICollection<LatestProductViewModel> GetHomePageLatestProducts()
        {
            var latestProducts = this.Context.Products.OrderByDescending(p => p.Id).Take(3).ToList();
            var latestProductsVms = new HashSet<LatestProductViewModel>();

            foreach (var product in latestProducts)
            {
                LatestProductViewModel lpvm = Mapper.Instance.Map<LatestProductViewModel>(product);
                lpvm.FinalPrice = this.CalculateFinalPrice(product.Discount, product.Price);
                lpvm.Description = product.Description.Substring(0, Math.Min(product.Description.Length, 180)) + "...";
                latestProductsVms.Add(lpvm);
            }

            return latestProductsVms;
        }

        public IQueryable<HardDrive> GetHardDrivesForApi(string driveBrand, string driveType)
        {
            HardDriveType hardDriveType = (HardDriveType)Enum.Parse(typeof(HardDriveType), driveType);
            HardDriveBrandType hardDriveBrandType = (HardDriveBrandType)Enum.Parse(typeof(HardDriveBrandType), driveBrand);
            return this.Context.HardDrives.Where(hd => hd.DriveBrand == hardDriveBrandType && hd.DriveType == hardDriveType);
        }

        public IQueryable<GraphicCard> GetGraphicCardsForApi(string memoryType, string brand, string manufacturer)
        {
            GraphicCardMemoryType graphicCardMemoryType = (GraphicCardMemoryType)Enum.Parse(typeof(GraphicCardMemoryType), memoryType);
            GraphicCardManufacturerType graphicCardManufacturerType = (GraphicCardManufacturerType)Enum.Parse(typeof(GraphicCardManufacturerType), brand);
            ManufacturerType manufacturerType = (ManufacturerType)Enum.Parse(typeof(ManufacturerType), manufacturer);
            return this.Context.GraphicCards.Where(gc => gc.MemoryType == graphicCardMemoryType && gc.Brand == graphicCardManufacturerType && gc.Manufacturer == manufacturerType);
        }

        public IQueryable<Processor> GetProcessorsForApi(string brand, string series, string cores)
        {
            ProcessorBrandType processorBrandType = (ProcessorBrandType)Enum.Parse(typeof(ProcessorBrandType), brand);
            ProcessorSeriesType processorSeriesType = (ProcessorSeriesType)Enum.Parse(typeof(ProcessorSeriesType), series);
            ProcessorCoresType processorCoresType = (ProcessorCoresType)Enum.Parse(typeof(ProcessorCoresType), cores.Replace("+", "_"));
            return this.Context.Processors.Where( p =>
                        p.Brand == processorBrandType && 
                        p.Cores == processorCoresType &&
                        p.Series == processorSeriesType);
        }
    }
}