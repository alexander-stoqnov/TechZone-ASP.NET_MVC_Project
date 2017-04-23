using System;

namespace TechZone.Models.EntityModels
{
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        public int Id { get; set; }
        
        [MinLength(10, ErrorMessage = "Please write at least 10 characters.")]
        [MaxLength(400, ErrorMessage = "Your comment cannot contain more than 400 characters.")]
        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        public virtual Review Review { get; set; } 

        public virtual Customer Customer { get; set; }
    }
}