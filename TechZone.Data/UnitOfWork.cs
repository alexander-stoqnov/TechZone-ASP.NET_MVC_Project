namespace TechZone.Data
{
    using Contracts;
    using Models.EntityModels;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly TechZoneContext context;
        private IRepository<ApplicationUser> users;
        private IRepository<Article> articles;
        private IRepository<Comment> comments;
        private IRepository<Customer> customers;
        private IRepository<GraphicCard> graphicCards;
        private IRepository<HardDrive> hardDrives;
        private IRepository<Processor> processors;
        private IRepository<Product> products;
        private IRepository<Purchase> purchases;
        private IRepository<Report> reports;
        private IRepository<Review> reviews;
        private IRepository<ShoppingCart> shoppingCarts;

        public UnitOfWork()
        {
            this.context = new TechZoneContext();
        }

        public IRepository<ApplicationUser> Users
            => this.users ?? (this.users = new Repository<ApplicationUser>(this.context.Users));
        public IRepository<Article> Articles
            => this.articles ?? (this.articles = new Repository<Article>(this.context.Articles));
        public IRepository<Comment> Comments
            => this.comments ?? (this.comments = new Repository<Comment>(this.context.Comments));
        public IRepository<Customer> Customers
            => this.customers ?? (this.customers = new Repository<Customer>(this.context.Customers));
        public IRepository<GraphicCard> GraphicCards
            => this.graphicCards ?? (this.graphicCards = new Repository<GraphicCard>(this.context.GraphicCards));
        public IRepository<HardDrive> HardDrives
            => this.hardDrives ?? (this.hardDrives = new Repository<HardDrive>(this.context.HardDrives));
        public IRepository<Processor> Processors
            => this.processors ?? (this.processors = new Repository<Processor>(this.context.Processors));
        public IRepository<Product> Products
            => this.products ?? (this.products = new Repository<Product>(this.context.Products));
        public IRepository<Purchase> Purchases
            => this.purchases ?? (this.purchases = new Repository<Purchase>(this.context.Purchases));
        public IRepository<Report> Reports
            => this.reports ?? (this.reports = new Repository<Report>(this.context.Reports));
        public IRepository<Review> Reviews
            => this.reviews ?? (this.reviews = new Repository<Review>(this.context.Reviews));
        public IRepository<ShoppingCart> ShoppingCarts
            => this.shoppingCarts ?? (this.shoppingCarts = new Repository<ShoppingCart>(this.context.ShoppingCarts));
       
        public int Commit()
        {
            return this.context.SaveChanges();
        }
    }
}