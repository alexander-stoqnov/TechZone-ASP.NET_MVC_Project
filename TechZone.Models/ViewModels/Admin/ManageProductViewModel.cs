namespace TechZone.Models.ViewModels.Admin
{
    public class ManageProductViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public bool IsAvailable { get; set; }
    }
}