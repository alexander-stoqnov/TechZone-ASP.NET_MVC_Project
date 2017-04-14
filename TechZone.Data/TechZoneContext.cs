namespace TechZone.Data
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models.EntityModels;

    public class TechZoneContext : IdentityDbContext<ApplicationUser>
    {
        public TechZoneContext()
            : base("name=TechZoneContext", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<GraphicCard> GraphicCards { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Publisher> Publishers { get; set; }

        public virtual DbSet<Article> Articles { get; set; }

        public virtual DbSet<Purchase> Purchases { get; set; }

        public static TechZoneContext Create()
        {
            return new TechZoneContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}