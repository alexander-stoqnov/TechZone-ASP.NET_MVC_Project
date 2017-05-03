namespace TechZone.Web.Tests.Controllers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Services;
    using Services.Contracts;
    using System.Web.Mvc;
    using AutoMapper;
    using Web.Controllers;
    using Models.EntityModels;
    using Models.ViewModels.Articles;
    using System.Collections.Generic;
    using TestStack.FluentMVCTesting;
    using System.Web;

    /// <summary>
    /// This tests the functionality of the articles controller
    /// </summary>
    [TestClass]
    public class ArticlesControllerTest
    {
        private readonly IArticlesService _service;
        private readonly ArticlesController _controller;

        public ArticlesControllerTest()
        {
            ConfigureMappings();
            this._service = new ArticlesService();
            this._controller = new ArticlesController(this._service);
        }

        [TestMethod]
        public void ArticlesAll_ShouldReturnItsDefaultView()
        {
            var result = _controller.All() as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName));
        }

        [TestMethod]
        public void ArticlesAll_ShouldReturnTheProperViewModel()
        {
            var result = _controller.All() as ViewResult;
            var vm = result.Model as IEnumerable<GeneralArticleViewModel>;
            Assert.IsNotNull(vm);
        }

        [TestMethod]
        public void ArticlesAdd_ShouldReturnItsDefaultView()
        {
            var result = _controller.Add() as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName));
        }

        [TestMethod]
        public void ArticlesSearch_ShouldRenderParticularPartial()
        {
            _controller.WithCallTo(pc => pc.Search(""))
                .ShouldRenderPartialView("_ArticleGeneralViewPartial");
        }

        [ExpectedException(typeof(HttpRequestValidationException))]
        public void ArticlesSearch_LookingForANaughtyStringShouldThrowError()
        {
            var result = _controller.Search("<br/>") as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName));
        }

        [TestMethod]
        public void ArticlesEdit_ShouldReturnItsDefaultView()
        {
            _controller.WithCallTo(pc => pc.EditArticle(3))
                .ShouldRenderDefaultView();
        }

        [TestMethod]
        public void ArticlesEdit_ShouldReturnViewWithEditArticleVm()
        {
            _controller.WithCallTo(pc => pc.EditArticle(10))
                .ShouldRenderDefaultView()
                .WithModel<EditArticleViewModel>(vm => vm.Id == 10);
        }

        [TestMethod]
        public void ArticlesDelete_ShouldReturnViewWithEditArticleVm()
        {
            _controller.WithCallTo(pc => pc.DeleteArticle(10))
                .ShouldRenderDefaultView()
                .WithModel<EditArticleViewModel>(vm => vm.Id == 10);
        }

        [TestMethod]
        public void ArticlesDelete_ShouldReturnItsDefaultView()
        {
            _controller.WithCallTo(pc => pc.DeleteArticle(10))
                .ShouldRenderDefaultView();
        }

        private void ConfigureMappings()
        {
            Mapper.Initialize(m =>
            {
                m.CreateMap<Article, GeneralArticleViewModel>()
                    .ForMember(gavm => gavm.AuthorName, expr => expr.MapFrom(a => a.Publisher.User.FullName))
                    .ForMember(gavm => gavm.AuthorUsername, expr => expr.MapFrom(a => a.Publisher.User.UserName));
                m.CreateMap<Article, EditArticleViewModel>();
            });
        }
    }
}
