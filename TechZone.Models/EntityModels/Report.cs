namespace TechZone.Models.EntityModels
{
    public class Report
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public virtual Customer Snitch { get; set; }

        public virtual Comment OffensiveComment { get; set; }
    }
}