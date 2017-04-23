namespace TechZone.Models.ViewModels.Reviews
{
    public class ReviewCommentViewModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string PublishDateString { get; set; }

        public string Commentor { get; set; }
    }
}