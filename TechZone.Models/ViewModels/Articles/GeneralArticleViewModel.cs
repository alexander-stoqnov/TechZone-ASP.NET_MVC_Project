namespace TechZone.Models.ViewModels.Articles
{
    using System;

    public class GeneralArticleViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string[] ContentParagraphs { get; set; }

        public string ImageData { get; set; }

        public DateTime? PublishDate { get; set; }

        public string AuthorName { get; set; }

        public string AuthorUsername { get; set; }
    }
}