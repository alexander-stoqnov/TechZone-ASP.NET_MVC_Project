namespace TechZone.Models.EntityModels
{
    using System.ComponentModel.DataAnnotations.Schema;
    using Enums;
    using System.ComponentModel.DataAnnotations;

    [Table("GraphicCard")]
    public class GraphicCard : Product
    {
        public GraphicCardMemoryType MemoryType { get; set; }

        public GraphicCardManufacturerType Brand { get; set; }

        public ManufacturerType Manufacturer { get; set; }

        [Required]
        [Range(1, 16, ErrorMessage = "Graphic Card memory should be between 1 and 16")]
        public int Memory { get; set; }
    }
}