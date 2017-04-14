namespace TechZone.Models.EntityModels
{
    public class Article
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public virtual Publisher Publisher { get; set; }
    }
}