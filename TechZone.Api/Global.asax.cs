namespace TechZone.Api
{
    using AutoMapper;
    using Models.EntityModels;
    using Models.ViewModels.Products;
    using System.Web.Http;

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ConfigureMappings();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        private void ConfigureMappings()
        {
            Mapper.Initialize(m =>
            {
                m.CreateMap<Product, GeneralProductPageViewModel>();
            });
        }
    }
}