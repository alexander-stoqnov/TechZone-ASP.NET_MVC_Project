namespace TechZone.Models.ViewModels.Customer
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CustomerPurchaseHistoryViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Purchase Date")]
        public DateTime PurchaseDate { get; set; }

        [Display(Name = "Total Cost")]
        public decimal FinalPrice { get; set; }
    }
}