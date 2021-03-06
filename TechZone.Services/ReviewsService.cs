﻿namespace TechZone.Services
{
    using System.Linq;
    using AutoMapper;
    using System;
    using System.Globalization;
    using Dropbox.Api;
    using System.Collections.Generic;
    using Models.ViewModels.Home;
    using Models.ViewModels.Reviews;
    using Models.EntityModels;
    using Models.BindingModels;
    using Contracts;

    public class ReviewsService : Service, IReviewsService
    {
        public bool ReviewExists(int? id)
        {
            return this.Context.Reviews.Find(id) != null;
        }

        public void CreateReview(string currentUserId, WriteReviewBindingModel wrbm)
        {
            var customer = this.Context.Customers.First(c => c.UserId == currentUserId);
            var product = this.Context.Products.Find(wrbm.ProductId);
            Review review = Mapper.Instance.Map<Review>(wrbm);

            review.Reviewer = customer;
            review.Product = product;
            if (product.Rating == 0 && !product.Reviews.Any())
            {
                product.Rating = review.Rating;
            }
            else
            {
                product.Rating = (decimal)(product.Reviews.Sum(r => r.Rating) + wrbm.Rating)/(product.Reviews.Count() + 1);
            }
            this.Context.Reviews.Add(review);
            this.Context.SaveChanges();
        }

        public ReviewOverviewViewModel GetReviewsForProduct(int id)
        {
            var product = this.Context.Products.Find(id);
            if (product.Reviews.Count == 0)
            {
                return new ReviewOverviewViewModel { AverageUserRating = 0};
            }
            ReviewOverviewViewModel rovm = new ReviewOverviewViewModel
            {
                AverageUserRating = product.Rating,
                NumberOf1Star = product.Reviews.Count(r => r.Rating == 1),
                NumberOf2Stars = product.Reviews.Count(r => r.Rating == 2),
                NumberOf3Stars = product.Reviews.Count(r => r.Rating == 3),
                NumberOf4Stars = product.Reviews.Count(r => r.Rating == 4),
                NumberOf5Stars = product.Reviews.Count(r => r.Rating == 5),
            };

            var reviews = product.Reviews.ToList();
            foreach (var review in reviews)
            {
                SimpleReviewViewModel srvm = Mapper.Instance.Map<SimpleReviewViewModel>(review);
                srvm.ReviewerUsername = review.Reviewer.User.UserName;
                srvm.DaysAgoPublished = (int)(DateTime.Now - review.PublishDate).TotalDays;
                srvm.PublishDateString = review.PublishDate.ToString("yy-MMM-dd ddd", new CultureInfo("en-US"));
                srvm.ContentParagraphs = review.Content.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                if (review.Reviewer.User.ProfilePictureFileName != null)
                {
                    srvm.ReviewerImageData = this.GetUserProfilePicture(review.Reviewer.User.ProfilePictureFileName,
                        review.Reviewer.User.UserName);
                }
                rovm.Reviews.Add(srvm);
            }

            return rovm;
        }

        private string GetUserProfilePicture(string profilePictureFileName, string userUserName)
        {
            var imageByteData = this.DownloadAsync(new DropboxClient("mQ4aAGajcfAAAAAAAAAAEcVfYBCEdnqccMa1IOiDpmOYVO6GkdprCUTg5p3GWMih"), $"Users/{userUserName}/ProfilePicture", profilePictureFileName);
            string imageBase64Data = Convert.ToBase64String(imageByteData.Result);
            return $"data:image/*;base64,{imageBase64Data}";
        }

        public SubmitReviewViewModel CheckWhetherUserHasReviewedProduct(string currentUserId, int id)
        {
            var customer = this.Context.Customers.First(c => c.UserId == currentUserId);
            var srvm = new SubmitReviewViewModel { Id = id };
            var review = this.Context.Reviews.FirstOrDefault(r => r.Reviewer.Id == customer.Id && r.Product.Id == id);
            if (review != null)
            {
                srvm.AlreadyReviewed = true;
                srvm.ReviewId = review.Id;
            }
            return srvm;
        }

        public bool HasUserReviewedProduct(string currentUserId, int id)
        {
            var customer = this.Context.Customers.First(c => c.UserId == currentUserId);
            return this.Context.Reviews.FirstOrDefault(r => r.Reviewer.Id == customer.Id && r.Product.Id == id) != null;
        }

        public ReviewDetailsViewModel GetReviewDetails(string currentUserId, int? id)
        {
            var customer = this.Context.Customers.FirstOrDefault(c => c.UserId == currentUserId);
            var review = this.Context.Reviews.Find(id);
            ReviewDetailsViewModel rdvm = Mapper.Instance.Map<ReviewDetailsViewModel>(review);
            rdvm.ContentParagraphs = review.Content.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
            if (customer != null)
            {
                rdvm.VisitorIsAlsoReviewPublisher = currentUserId == review.Reviewer.UserId;
                rdvm.IsVisitorAllowedToPostComments = customer.Warnings <= 3;
            }
            else
            {
                rdvm.IsVisitorAllowedToPostComments = true;
            }
            if (review.Reviewer.User.ProfilePictureFileName != null)
            {
                rdvm.ReviewerImageData = this.GetUserProfilePicture(review.Reviewer.User.ProfilePictureFileName,
                    review.Reviewer.User.UserName);
            }
            rdvm.ReviewComments = Mapper.Instance.Map<IEnumerable<ReviewCommentViewModel>>(review.Comments);
            return rdvm;
        }

        public void WriteCommentToReview(string currentUserId, AddCommentBindingModel acbm)
        {
            var customer = this.Context.Customers.First(c => c.UserId == currentUserId);
            var review = this.Context.Reviews.Find(acbm.Id);
            Comment comment = new Comment
            {
                Review = review,
                Content = acbm.Comment,
                Customer = customer,
                PublishDate = DateTime.Now
            };
            this.Context.Comments.Add(comment);
            this.Context.SaveChanges();
        }

        public bool UserHasAlreadyVotedForReview(string currentUserId, int id)
        {
            var customer = this.Context.Customers.First(c => c.UserId == currentUserId);
            return customer.VotedFor.Any(r => r.Id == id);
        }

        public void CastUserVote(string currentUserId, VoteForReviewViewModel vote)
        {
            var customer = this.Context.Customers.First(c => c.UserId == currentUserId);
            var review = this.Context.Reviews.Find(vote.Id);

            if (vote.Vote == "up")
            {
                review.Useful++;
            }
            else
            {
                review.Useless++;
            }
            customer.VotedFor.Add(review);
            this.Context.SaveChanges();
        }

        public ICollection<LatestReviewViewModel> GetHomePageLatestReviews()
        {
            var latestReviews = this.Context.Reviews.OrderByDescending(r => r.PublishDate).Take(3).ToList();
            var latestReviewsVm = new HashSet<LatestReviewViewModel>();

            foreach (var review in latestReviews)
            {
                var reviewVm = Mapper.Instance.Map<LatestReviewViewModel>(review);
                reviewVm.Content = review.Content.Substring(0, Math.Min(review.Content.Length, 250)) + "...";
                latestReviewsVm.Add(reviewVm);
            }
            return latestReviewsVm;
        }

        public bool IsCurrentUserAllowedToComment(string currentUserId)
        {
            var customer = this.Context.Customers.First(c => c.UserId == currentUserId);
            return customer.Warnings <= 3;
        }
    }
}