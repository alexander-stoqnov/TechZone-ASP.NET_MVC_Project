namespace TechZone.Services
{
    using Models.ViewModels.Moderator;
    using System.Linq;
    using AutoMapper;
    using Models.EntityModels;
    using System.Collections.Generic;

    public class ModeratorService : Service
    {
        public SubmitReportViewModel PrepareSubmitReportInfo(int id)
        {
            var comment = this.Context.Comments.Find(id);
            return new SubmitReportViewModel
            {
                ReportedCommentId = comment.Id,
                ReviewId = comment.Review.Id
            };
        }

        public void SendCommentReport(string currentUserId, SubmitReportViewModel srbm)
        {
            var customer = this.Context.Customers.First(c => c.UserId == currentUserId);
            var comment = this.Context.Comments.Find(srbm.ReportedCommentId);

            Report report = Mapper.Instance.Map<Report>(srbm);
            report.Snitch = customer;
            report.OffensiveComment = comment;

            this.Context.Reports.Add(report);
            this.Context.SaveChanges();
        }

        public IEnumerable<EvaluateReportViewModel> GetAllUnevaluatedReports()
        {
            var reportEntities = this.Context.Reports.Where(r => !r.IsEvaluated);
            return Mapper.Instance.Map<IEnumerable<EvaluateReportViewModel>>(reportEntities);
        }
    }
}