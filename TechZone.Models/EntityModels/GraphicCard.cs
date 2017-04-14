namespace TechZone.Models.EntityModels
{
    using System.ComponentModel.DataAnnotations.Schema;
    using Enums;
    using System.Collections.Generic;

    [Table("GraphicCard")]
    public class GraphicCard : Product
    {
        public GraphicCardMemoryType MemoryType { get; set; }

        public GraphicCardManufacturerType Brand { get; set; }

        public IEnumerable<GraphicCardSlotType> Ports { get; set; }

        public ManufacturerType Manufacturer { get; set; }

        public int Memory { get; set; }
    }
}