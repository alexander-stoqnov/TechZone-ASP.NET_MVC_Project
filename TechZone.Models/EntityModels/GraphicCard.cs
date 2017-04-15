namespace TechZone.Models.EntityModels
{
    using System.ComponentModel.DataAnnotations.Schema;
    using Enums;

    [Table("GraphicCard")]
    public class GraphicCard : Product
    {
        public GraphicCardMemoryType MemoryType { get; set; }

        public GraphicCardManufacturerType Brand { get; set; }

        public ManufacturerType Manufacturer { get; set; }

        public int Memory { get; set; }
    }
}