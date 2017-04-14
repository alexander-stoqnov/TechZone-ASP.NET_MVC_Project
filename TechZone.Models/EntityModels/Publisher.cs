namespace TechZone.Models.EntityModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Publisher
    {
        public Publisher()
        {
            this.Articles = new HashSet<Article>();
        }

        public int Id { get; set; }

        public string Biography { get; set; }

        public string ImageUrl { get; set; }

        public virtual ICollection<Article> Articles { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}