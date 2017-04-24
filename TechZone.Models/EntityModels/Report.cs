using System.ComponentModel.DataAnnotations;

namespace TechZone.Models.EntityModels
{
    public class Report
    {
        public int Id { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Report title should be at least 10 characters long")]
        [MaxLength(50, ErrorMessage = "Report title should be less than 50 characters long")]
        public string Title { get; set; }

        [Required]
        [MinLength(20, ErrorMessage = "Description should be at least 20 characters long")]
        [MaxLength(300, ErrorMessage = "Description should be less than 300 characters long")]
        public string Description { get; set; }

        public virtual Customer Snitch { get; set; }

        public virtual Comment OffensiveComment { get; set; }
    }
}