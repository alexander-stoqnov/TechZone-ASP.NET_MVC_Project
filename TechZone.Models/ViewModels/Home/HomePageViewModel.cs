namespace TechZone.Models.ViewModels.Home
{
    using System.Collections.Generic;

    public class HomePageViewModel
    {
        public HomePageViewModel()
        {
            this.LatestProducts = new HashSet<LatestProductViewModel>();
            this.LatestReviews = new HashSet<LatestReviewViewModel>();
            this.LatestArticles = new List<LatestArticleViewModel>();
        }

        public ICollection<LatestProductViewModel> LatestProducts { get; set; }

        public ICollection<LatestReviewViewModel> LatestReviews { get; set; }

        public List<LatestArticleViewModel> LatestArticles { get; set; }
    }
}
