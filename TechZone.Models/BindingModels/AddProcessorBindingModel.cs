namespace TechZone.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;
    using Enums;


    public class AddProcessorBindingModel : AddProductBindingModel
    {
        public ProcessorSeriesType Series { get; set; }

        public ProcessorBrandType Brand { get; set; }

        public ProcessorCoresType Cores { get; set; }

        [Required]
        [Range(1, 32, ErrorMessage = "Cache should be between 1 and 32 Mb")]
        public int Cache { get; set; }

        [Required]
        [Display(Name = "Clock Speed")]
        [Range(1, 16, ErrorMessage = "Processor clock speed be between 1 and 16 Ghz")]
        public decimal ProcessorSpeed { get; set; }
    }
}