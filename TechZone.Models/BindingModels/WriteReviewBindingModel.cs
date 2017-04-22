namespace TechZone.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class WriteReviewBindingModel
    {
        [Required]
        [Range(1, 5, ErrorMessage = "Please give only a rating between 1 and 5")]
        public int Rating { get; set; }

        [Required]
        [MinLength(100, ErrorMessage = "Please write at least 100 characters.")]
        [MaxLength(3000, ErrorMessage = "Review cannot be more than 3000 characters long")]
        public string Content { get; set; }

        [Required]
        public int ProductId { get; set; }
    }
}