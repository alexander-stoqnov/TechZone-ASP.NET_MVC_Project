namespace TechZone.Models.ViewModels.Reviews
{
    using System.Collections.Generic;

    public class ReviewOverviewViewModel
    {
        public ReviewOverviewViewModel()
        {
            this.Reviews = new HashSet<SimpleReviewViewModel>();
        }

        public decimal AverageUserRating { get; set; }

        public int NumberOf5Stars { get; set; }
        public int NumberOf4Stars { get; set; }
        public int NumberOf3Stars { get; set; }
        public int NumberOf2Stars { get; set; }
        public int NumberOf1Star { get; set; }

        public ICollection<SimpleReviewViewModel> Reviews { get; set; }
    }
}