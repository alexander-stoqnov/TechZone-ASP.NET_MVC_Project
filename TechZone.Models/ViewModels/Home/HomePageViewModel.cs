using System.Collections.Generic;

namespace TechZone.Models.ViewModels.Home
{
    public class HomePageViewModel
    {
        public HomePageViewModel()
        {
            this.LatestProducts = new HashSet<LatestProductViewModel>();
            this.LatestReviews = new HashSet<LatestReviewViewModel>();
        }

        public ICollection<LatestProductViewModel> LatestProducts { get; set; }

        public ICollection<LatestReviewViewModel> LatestReviews { get; set; }
    }
}
