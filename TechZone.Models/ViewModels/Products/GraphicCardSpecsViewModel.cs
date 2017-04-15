namespace TechZone.Models.ViewModels.Products
{
    using Enums;

    public class GraphicCardSpecsViewModel
    {
        public GraphicCardMemoryType MemoryType { get; set; }

        public GraphicCardManufacturerType Brand { get; set; }

        public ManufacturerType Manufacturer { get; set; }

        public int Memory { get; set; }
    }
}