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

        public ReviewOverviewViewModel GetReviewsForProduct(int id)
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
                rovm.Reviews.Add(srvm);
            }

            return rovm;
        }
    }
}