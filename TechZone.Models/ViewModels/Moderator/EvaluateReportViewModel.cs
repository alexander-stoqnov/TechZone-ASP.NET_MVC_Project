namespace TechZone.Models.ViewModels.Moderator
{
    using System.ComponentModel.DataAnnotations;

    public class EvaluateReportViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [Display(Name = "Submitter")]
        public string SubmittedBy { get; set; }

        [Display(Name = "Comment")]
        public string CommentContent { get; set; }

        [Display(Name = "Author")]
        public string CommentOffender { get; set; }
        
        public int ReviewId { get; set; }
    }
}