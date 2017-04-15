namespace TechZone.Models.ViewModels.Products
{
    public class GeneralProductPageViewModel
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public decimal FinalPrice { get; set; }

        public int Discount { get; set; }

        public string Name { get; set; }

        public decimal Rating { get; set; }

        public string ImageUrl { get; set; }
    }
}