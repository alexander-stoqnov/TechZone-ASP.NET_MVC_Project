namespace TechZone.Models.ViewModels.Purchase
{
    public class ProductInCartViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public int Discount { get; set; }

        public decimal FinalPrice { get; set; }
    }
}