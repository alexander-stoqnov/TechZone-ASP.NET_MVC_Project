namespace TechZone.Services.Contracts
{
    using System.Collections.Generic;
    using Models.BindingModels;
    using Models.ViewModels.Home;
    using Models.ViewModels.Reviews;

    public interface IReviewsService
    {
        bool ReviewExists(int id);
        void CreateReview(string currentUserId, WriteReviewBindingModel wrbm);
        ReviewOverviewViewModel GetReviewsForProduct(int id);
        SubmitReviewViewModel CheckWhetherUserHasReviewedProduct(string currentUserId, int id);
        bool HasUserReviewedProduct(string currentUserId, int id);
        ReviewDetailsViewModel GetReviewDetails(string currentUserId, int id);
        void WriteCommentToReview(string currentUserId, AddCommentBindingModel acbm);
        bool UserHasAlreadyVotedForReview(string currentUserId, int id);
        void CastUserVote(string currentUserId, VoteForReviewViewModel vote);
        ICollection<LatestReviewViewModel> GetHomePageLatestReviews();
        bool IsCurrentUserAllowedToComment(string currentUserId);
    }
}