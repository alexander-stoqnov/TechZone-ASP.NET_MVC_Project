﻿using TechZone.Models.BindingModels;
using TechZone.Models.ViewModels.Reviews;

namespace TechZone.Web
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using AutoMapper;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models.ViewModels.Admin;
    using Models.EntityModels;
    using Models.ViewModels.Products;
    using System;
    using Models.ViewModels.Purchase;
    using Models.ViewModels.Customer;

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
                m.CreateMap<Purchase, CustomerPurchaseHistoryViewModel>();
                m.CreateMap<Customer, CustomerProfileViewModel>()
                .ForMember(cpvm => cpvm.FullName, expr => expr.MapFrom(c => c.User.FullName))
                .ForMember(cpvm => cpvm.Email, expr => expr.MapFrom(c => c.User.Email))
                .ForMember(cpvm => cpvm.Username, expr => expr.MapFrom(c => c.User.UserName));
                m.CreateMap<Review, SimpleReviewViewModel>();

                m.CreateMap<WriteReviewBindingModel, Review>();
            });
        }
    }
}