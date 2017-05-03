namespace TechZone.Services
{
    using Models.ViewModels.Moderator;
    using System.Linq;
    using AutoMapper;
    using Models.EntityModels;
    using System.Collections.Generic;
    using Contracts;

    public class ModeratorService : Service, IModeratorService
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

        public bool ReportStillExists(int id)
        {
            return this.Context.Reports.Any(r => r.Id == id);
        }

        public void RemoveReport(int id)
        {
            var report = this.Context.Reports.Find(id);
            this.Context.Reports.Remove(report);
            this.Context.SaveChanges();
        }

        public void IssueWarningToCustomer(int id)
        {
            var report = this.Context.Reports.Find(id);
            var customer = report.OffensiveComment.Customer;
            customer.Warnings++;
            this.Context.Reports.Remove(report);
            this.Context.SaveChanges();
        }

        public bool IsRoomForCurrentUser(string currentUserId, string roomId)
        {
            var customer = this.Context.Customers.First(c => c.UserId == currentUserId);
            ForgivenessRequest frq = this.Context.ForgivenessRequests.FirstOrDefault(fr => fr.RoomId == roomId);
            return frq != null && frq.Customer == customer && !frq.IsAnswered;
        }

        public void RemoveUserWarnings(string roomId)
        {
            var frq = this.Context.ForgivenessRequests.First(c => c.RoomId == roomId);
            frq.Customer.Warnings = 0;
            var forgivnesses = this.Context.ForgivenessRequests.Where(freq => freq.Customer.Id == frq.Customer.Id);
            foreach (var forgivness in forgivnesses)
            {
                forgivness.IsAnswered = true;
            }
            this.Context.SaveChanges();
        }
    }
}