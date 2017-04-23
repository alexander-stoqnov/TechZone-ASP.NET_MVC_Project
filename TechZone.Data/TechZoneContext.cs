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

        public virtual DbSet<HardDrive> HardDrives { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Publisher> Publishers { get; set; }

        public virtual DbSet<Article> Articles { get; set; }

        public virtual DbSet<Purchase> Purchases { get; set; }

        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }

        public virtual DbSet<Review> Reviews { get; set; }

        public virtual DbSet<Comment> Comments { get; set; }

        public virtual DbSet<Report> Reports { get; set; }

        public static TechZoneContext Create()
        {
            return new TechZoneContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>()
                .HasRequired(r => r.Reviewer)
                .WithMany(c => c.WrittenReviews)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Review>()
                .HasMany(r => r.VotedBy)
                .WithMany(c => c.VotedFor)
                .Map(rc => rc.MapLeftKey("ReviewId")
                    .MapRightKey("VoterId")
                    .ToTable("ReviewsVoters"));

            base.OnModelCreating(modelBuilder);
        }
    }
}