namespace TechZone.Models.ViewModels.Customer
{
    using System.Collections.Generic;

    public class CustomerProfileViewModel
    {
        public CustomerProfileViewModel()
        {
            this.PurchasesHistory = new HashSet<CustomerPurchaseHistoryViewModel>();
        }

        public string FullName { get; set; }

        public string ImageData { get; set; }

        public decimal Credits { get; set; }

        public virtual ICollection<CustomerPurchaseHistoryViewModel> PurchasesHistory { get; set; }
    }
}