namespace TechZone.Services.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using Models.EntityModels;
    using Models.ViewModels.Home;
    using Models.ViewModels.Products;

    public interface IProductsService
    {
        IEnumerable<GeneralProductPageViewModel> GetAllProducts();
        IEnumerable<GeneralProductPageViewModel> GetAllGraphicCards();
        IEnumerable<GeneralProductPageViewModel> GetAllHardDrives();
        IEnumerable<GeneralProductPageViewModel> GetAllProcessors();
        HashSet<GeneralProductPageViewModel> GetGeneralProductPageViewModels<T>(List<T> products) where T : Product;
        bool ProductExists(int id);
        ProductDetailsViewModel GetProductDetails(int id);
        Dictionary<string, string> GetProductSpecs(int id);
        ICollection<LatestProductViewModel> GetHomePageLatestProducts();
        IQueryable<HardDrive> GetHardDrivesForApi(string driveBrand, string driveType);
        IQueryable<GraphicCard> GetGraphicCardsForApi(string memoryType, string brand, string manufacturer);
        IQueryable<Processor> GetProcessorsForApi(string brand, string series, string cores);
    }
}