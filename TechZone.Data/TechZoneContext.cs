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

        public static TechZoneContext Create()
        {
            return new TechZoneContext();
        }
    }
}