namespace TechZone.Models.ViewModels.Articles
{
    using System;

    public class GeneralArticleViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public DateTime? PublishDate { get; set; }

        public string AuthorName { get; set; }
    }
}