namespace TechZone.Models.EntityModels
{
    using Enums;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Processor")]
    public class Processor : Product
    {
        public ProcessorSeriesType Series { get; set; }

        public ProcessorBrandType Brand { get; set; }

        public ProcessorCoresType Cores { get; set; }

        public int Cache { get; set; }

        public decimal ProcessorSpeed { get; set; }
    }
}