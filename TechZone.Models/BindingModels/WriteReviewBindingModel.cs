namespace TechZone.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class WriteReviewBindingModel
    {
        [Required]
        [Range(1, 5, ErrorMessage = "Please give only a rating between 1 and 5")]
        public int Rating { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Title must be at least 10 characters long")]
        [MaxLength(50, ErrorMessage = "Title cannot be longer than 50 characters")]
        public string Title { get; set; }

        [Required]
        [MinLength(100, ErrorMessage = "Please write at least 100 characters.")]
        [MaxLength(3000, ErrorMessage = "Review cannot be more than 3000 characters long")]
        public string Content { get; set; }

        [Required]
        public int ProductId { get; set; }
    }
}