namespace TechZone.Data.Contracts
{
    using Models.EntityModels;

    public interface IUnitOfWork
    {
        IRepository<ApplicationUser> Users { get; }

        IRepository<Article> Articles { get; }

        IRepository<Comment> Comments { get; }

        IRepository<Customer> Customers { get; }

        IRepository<GraphicCard> GraphicCards { get; }

        IRepository<HardDrive> HardDrives { get; }

        IRepository<Processor> Processors { get; }

        IRepository<Product> Products { get; }

        IRepository<Purchase> Purchases { get; }

        IRepository<Report> Reports { get; }

        IRepository<Review> Reviews { get; }

        IRepository<ShoppingCart> ShoppingCarts { get; }

        int Commit();
    }
}
