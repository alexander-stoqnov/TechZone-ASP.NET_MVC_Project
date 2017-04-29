namespace TechZone.Models.ViewModels.Home
{
    public class LatestArticleViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string[] ContentParagraphs { get; set; }

        public string ImageData { get; set; }
    }
}