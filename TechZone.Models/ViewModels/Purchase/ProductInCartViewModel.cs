namespace TechZone.Models.ViewModels.Purchase
{
    using Enums;

    public class ProductInCartViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public int Discount { get; set; }

        public int Quantity { get; set; }

        public GuaranteeDurationType Guarantee { get; set; }

        public decimal FinalPrice { get; set; }

        public bool IsAvailable { get; set; }
    }
}