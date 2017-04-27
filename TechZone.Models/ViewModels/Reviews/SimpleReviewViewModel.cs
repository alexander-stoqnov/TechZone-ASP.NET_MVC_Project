namespace TechZone.Models.ViewModels.Reviews
{
    public class SimpleReviewViewModel
    {
        public int Id { get; set; }

        public string PublishDateString { get; set; }

        public int Rating { get; set; }

        public string[] ContentParagraphs { get; set; }

        public string Title { get; set; }

        public int DaysAgoPublished { get; set; }

        public string ReviewerUsername { get; set; }

        public string ReviewerImageData { get; set; }
    }
}