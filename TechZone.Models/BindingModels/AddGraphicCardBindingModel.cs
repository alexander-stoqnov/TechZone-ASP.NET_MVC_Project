namespace TechZone.Models.BindingModels
{
    using Enums;
    using System.ComponentModel.DataAnnotations;

    public class AddGraphicCardBindingModel : AddProductBindingModel
    {
        public GraphicCardMemoryType MemoryType { get; set; }

        public GraphicCardManufacturerType Brand { get; set; }

        public ManufacturerType Manufacturer { get; set; }

        [Required]
        [Range(1, 16, ErrorMessage = "Graphic Card memory should be between 1 and 16")]
        public int Memory { get; set; }
    }
}
