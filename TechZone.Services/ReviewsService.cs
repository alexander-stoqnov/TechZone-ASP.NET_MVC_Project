namespace TechZone.Services
{
    using Models.BindingModels;
    using System.Linq;
    using AutoMapper;
    using Models.EntityModels;

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
    }
}