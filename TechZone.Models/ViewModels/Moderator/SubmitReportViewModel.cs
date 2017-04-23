namespace TechZone.Models.ViewModels.Moderator
{
    public class SubmitReportViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int SnitchId { get; set; }

        public int ReportedCommentId { get; set; }
    }
}