namespace TechZone.Models.BindingModels
{
    public class WriteReviewBindingModel
    {
        public int Rating { get; set; }

        public string Content { get; set; }

        public int ProductId { get; set; }
    }
}