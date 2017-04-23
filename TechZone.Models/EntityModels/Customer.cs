namespace TechZone.Models.EntityModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Customer
    {
        public Customer()
        {
            this.Purchases = new HashSet<Purchase>();
            this.VotedFor = new HashSet<Review>();
            this.WrittenReviews = new HashSet<Review>();
            this.Comments = new HashSet<Comment>();
        }

        public int Id { get; set; }

        public int Warnings { get; set; }

        public decimal Credits { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }

        public virtual ICollection<Review> VotedFor { get; set; }

        public virtual ICollection<Review> WrittenReviews { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}