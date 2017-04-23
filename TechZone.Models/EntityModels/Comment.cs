namespace TechZone.Models.EntityModels
{
    using System.ComponentModel.DataAnnotations;
    using System;

    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Please write at least 10 characters.")]
        [MaxLength(400, ErrorMessage = "Your comment cannot contain more than 400 characters.")]
        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        public virtual Review Review { get; set; } 

        public virtual Customer Customer { get; set; }
    }
}