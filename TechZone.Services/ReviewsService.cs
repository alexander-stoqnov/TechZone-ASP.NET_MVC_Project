using Dropbox.Api;

namespace TechZone.Services
{
    using Models.BindingModels;
    using System.Linq;
    using AutoMapper;
    using Models.EntityModels;
    using System;
    using Models.ViewModels.Reviews;

    public class ReviewsService : Service
    {
        public void CreateReview(string currentUserId, WriteReviewBindingModel wrbm)
        {
            var customer = this.Context.Customers.First(c => c.UserId == currentUserId);
            var product = this.Context.Products.Find(wrbm.ProductId);
            Review review = Mapper.Instance.Map<Review>(wrbm);

            review.Reviewer = customer;
            review.Product = product;
            this.Context.Reviews.Add(review);
            this.Context.SaveChanges();
        }

        public ReviewOverviewViewModel GetReviewsForProduct(int id, string dropboxKey)
        {
            var product = this.Context.Products.Find(id);
            if (product.Reviews.Count == 0)
            {
                return new ReviewOverviewViewModel { AverageUserRating = 0};
            }
            ReviewOverviewViewModel rovm = new ReviewOverviewViewModel
            {
                AverageUserRating = (decimal)product.Reviews.Average(r => r.Rating),
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
                if (review.Reviewer.User.ProfilePictureFileName != null)
                {
                    srvm.ReviewerImageData = this.GetUserProfilePicture(review.Reviewer.User.ProfilePictureFileName,
                        review.Reviewer.User.UserName, dropboxKey);
                }
                rovm.Reviews.Add(srvm);
            }

            return rovm;
        }

        private string GetUserProfilePicture(string profilePictureFileName, string userUserName, string dropboxKey)
        {
            var imageByteData = this.DownloadAsync(new DropboxClient(dropboxKey), $"Users/{userUserName}/ProfilePicture", profilePictureFileName);
            string imageBase64Data = Convert.ToBase64String(imageByteData.Result);
            return $"data:image/*;base64,{imageBase64Data}";
        }
    }
}