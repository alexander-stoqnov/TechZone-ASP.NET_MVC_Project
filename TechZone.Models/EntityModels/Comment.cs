namespace TechZone.Models.EntityModels
{
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Your desired alies must be at least 5 symbols long")]
        public string AuthorAlias { get; set; }
        
        [MinLength(10, ErrorMessage = "Please write at least 10 characters.")]
        [MaxLength(400, ErrorMessage = "Your comment cannot contain more than 400 characters.")]
        public string Content { get; set; }

        public virtual Review Review { get; set; } 
    }
}