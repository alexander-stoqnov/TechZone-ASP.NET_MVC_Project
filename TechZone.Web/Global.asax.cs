namespace TechZone.Web
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using System;
    using System.Globalization;
    using AutoMapper;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models.EntityModels;
    using Models.ViewModels.Products;
    using Models.ViewModels.Purchase;
    using Models.ViewModels.Customer;
    using Models.ViewModels.Admin;
    using Models.ViewModels.Reviews;
    using Models.ViewModels.Moderator;
    using Models.ViewModels.Articles;
    using Models.ViewModels.Home;
    using Models.BindingModels;

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            ConfigureMappings();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        void Session_Start(object sender, EventArgs e)
        {
            HttpContext.Current.Session.Add("__MyAppSession", string.Empty);
        }

        private void ConfigureMappings()
        {
            Mapper.Initialize(m =>
            {
                m.CreateMap<IdentityRole, RoleViewModel>();
                m.CreateMap<Product, GeneralProductPageViewModel>();
                m.CreateMap<Product, ProductDetailsViewModel>();
                m.CreateMap<Product, ProductInCartViewModel>();
                m.CreateMap<Product, ManageProductViewModel>();
                m.CreateMap<Product, EditProductViewModel>();
                m.CreateMap<Product, LatestProductViewModel>();
                m.CreateMap<Purchase, CustomerPurchaseHistoryViewModel>();
                m.CreateMap<Customer, CustomerProfileViewModel>()
                .ForMember(cpvm => cpvm.FullName, expr => expr.MapFrom(c => c.User.FullName))
                .ForMember(cpvm => cpvm.Email, expr => expr.MapFrom(c => c.User.Email))
                .ForMember(cpvm => cpvm.Username, expr => expr.MapFrom(c => c.User.UserName));
                m.CreateMap<Review, SimpleReviewViewModel>();
                m.CreateMap<Review, LatestReviewViewModel>()
                .ForMember(lrvm => lrvm.ProductImage, expr => expr.MapFrom(r => r.Product.ImageUrl))
                .ForMember(lrvm => lrvm.ProductName, expr => expr.MapFrom(r => r.Product.Name))
                .ForMember(lrvm => lrvm.ProductId, expr => expr.MapFrom(r => r.Product.Id));
                m.CreateMap<Review, ReviewDetailsViewModel>()
                .ForMember(rdvm => rdvm.CountOfComments, expr => expr.MapFrom(r => r.Comments.Count))
                .ForMember(rdvm => rdvm.ReviewerUsername, expr => expr.MapFrom(r => r.Reviewer.User.UserName))
                .ForMember(rdvm => rdvm.PublishDateString, expr => expr.MapFrom(c => c.PublishDate.ToString("dd MMMM yyyy", new CultureInfo("en-US"))));
                m.CreateMap<Comment, ReviewCommentViewModel>()
                .ForMember(rcvm => rcvm.Commentor, expr => expr.MapFrom(c => c.Customer.User.UserName))
                .ForMember(rcvm => rcvm.PublishDateString, expr => expr.MapFrom(c => c.PublishDate.ToString("dd MMMM yyyy", new CultureInfo("en-US"))));
                m.CreateMap<Report, EvaluateReportViewModel>()
                    .ForMember(ervm => ervm.CommentContent, expr => expr.MapFrom(r => r.OffensiveComment.Content))
                    .ForMember(ervm => ervm.SubmittedBy, expr => expr.MapFrom(r => r.Snitch.User.UserName))
                    .ForMember(ervm => ervm.CommentOffender, expr => expr.MapFrom(r => r.OffensiveComment.Customer.User.UserName))
                    .ForMember(ervm => ervm.ReviewId, expr => expr.MapFrom(r => r.OffensiveComment.Review.Id));
                m.CreateMap<Article, GeneralArticleViewModel>()
                    .ForMember(gavm => gavm.AuthorName, expr => expr.MapFrom(a => a.Publisher.User.FullName))
                    .ForMember(gavm => gavm.AuthorUsername, expr => expr.MapFrom(a => a.Publisher.User.UserName));
                m.CreateMap<Article, EditArticleViewModel>();
                m.CreateMap<Article, LatestArticleViewModel>();
                m.CreateMap<ForgivenessRequest, UserWarningsViewModel>()
                 .ForMember(uwvm => uwvm.CountOfWarnings, expr => expr.MapFrom(fr => fr.Customer.Warnings))
                 .ForMember(uwvm => uwvm.WarnedUserUsername, expr => expr.MapFrom(fr => fr.Customer.User.UserName));

                m.CreateMap<WriteReviewBindingModel, Review>();
                m.CreateMap<SubmitReportViewModel, Report>();
                m.CreateMap<AddHardDriveBindingModel, HardDrive>();
                m.CreateMap<AddGraphicCardBindingModel, GraphicCard>();
                m.CreateMap<AddProcessorBindingModel, Processor>();
                m.CreateMap<AddArticleViewModel, Article>();
            });
        }
    }
}