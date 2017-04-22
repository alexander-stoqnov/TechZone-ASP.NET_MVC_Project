namespace TechZone.Models.ViewModels.Reviews
{
    using System;

    public class SimpleReviewViewModel
    {
        public int Id { get; set; }

        public DateTime PublishDate { get; set; }

        public int Rating { get; set; }

        public string Content { get; set; }

        public int DaysAgoPublished { get; set; }

        public string ReviewerUsername { get; set; }

        public string ReviewerImageData { get; set; }
    }
}