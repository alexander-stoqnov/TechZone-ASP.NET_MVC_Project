namespace TechZone.Models.ViewModels.Customer
{
    using System;

    public class CustomerPurchaseHistoryViewModel
    {
        public int Id { get; set; }

        public DateTime PurchaseDate { get; set; }

        public decimal TotalCost { get; set; }
    }
}