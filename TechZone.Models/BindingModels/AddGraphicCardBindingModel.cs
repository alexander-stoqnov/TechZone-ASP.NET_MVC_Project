namespace TechZone.Models.BindingModels
{
    using Enums;

    public class AddGraphicCardBindingModel : AddProductBindingModel
    {
        public GraphicCardMemoryType MemoryType { get; set; }

        public GraphicCardManufacturerType Brand { get; set; }

        public ManufacturerType Manufacturer { get; set; }

        public int Memory { get; set; }
    }
}
