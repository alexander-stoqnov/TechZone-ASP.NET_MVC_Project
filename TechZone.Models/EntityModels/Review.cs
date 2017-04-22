namespace TechZone.Models.EntityModels
{
    using System.Collections.Generic;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Review
    {
        public Review()
        {
            this.Comments = new HashSet<Comment>();
            this.PublishDate = DateTime.Now;
        }

        public int Id { get; set; }

        [Required]
        [MinLength(100, ErrorMessage = "Please write at least 100 characters.")]
        [MaxLength(3000, ErrorMessage = "Review cannot be more than 3000 characters long")]
        public string Content { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Please give only a rating between 1 and 5")]
        public int Rating { get; set; }

        [Range(0, Int32.MaxValue)]
        public int Useful { get; set; }

        [Range(0, Int32.MaxValue)]
        public int Useless { get; set; }

        public DateTime PublishDate { get; set; }

        public virtual Product Product { get; set; }

        public virtual Customer Reviewer { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Customer> VotedBy { get; set; }
    }
}