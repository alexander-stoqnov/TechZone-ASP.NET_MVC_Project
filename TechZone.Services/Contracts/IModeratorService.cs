namespace TechZone.Services.Contracts
{
    using System.Collections.Generic;
    using Models.ViewModels.Moderator;

    public interface IModeratorService
    {
        SubmitReportViewModel PrepareSubmitReportInfo(int id);
        void SendCommentReport(string currentUserId, SubmitReportViewModel srbm);
        IEnumerable<EvaluateReportViewModel> GetAllUnevaluatedReports();
        bool ReportStillExists(int id);
        void RemoveReport(int id);
        void IssueWarningToCustomer(int id);
        bool IsRoomForCurrentUser(string currentUserId, string roomId);
        void RemoveUserWarnings(string roomId);
        IEnumerable<UserWarningsViewModel> GetAllUserWarnings();
    }
}