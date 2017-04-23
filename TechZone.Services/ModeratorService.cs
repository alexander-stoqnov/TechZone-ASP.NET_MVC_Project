namespace TechZone.Services
{
    using System.Linq;
    using Models.ViewModels.Moderator;

    public class ModeratorService : Service
    {
        public SubmitReportViewModel PrepareSubmitReportInfo(string currentUserId, int id)
        {
            var customer = this.Context.Customers.First(c => c.UserId == currentUserId);
            var comment = this.Context.Comments.Find(id);
            return new SubmitReportViewModel
            {
                ReportedCommentId = comment.Id,
                SnitchId = customer.Id
            };
        }
    }
}