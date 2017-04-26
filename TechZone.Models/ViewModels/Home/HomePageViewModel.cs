using System.Collections.Generic;

namespace TechZone.Models.ViewModels.Home
{
    public class HomePageViewModel
    {
        public HomePageViewModel()
        {
            this.LatestProducts = new HashSet<LatestProductsViewModel>();
        }

        public ICollection<LatestProductsViewModel> LatestProducts { get; set; }
    }
}
