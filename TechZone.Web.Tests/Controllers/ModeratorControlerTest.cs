namespace TechZone.Web.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Net;
    using AutoMapper;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models.EntityModels;
    using Models.ViewModels.Admin;
    using Models.ViewModels.Moderator;
    using Services;
    using Services.Contracts;
    using Areas.Moderator.Controllers;
    using Web.Controllers;
    using TestStack.FluentMVCTesting;

    /// <summary>
    /// Testing the functionality of the moderator area
    /// </summary>
    [TestClass]
    public class ModeratorControlerTest
    {
        private readonly IModeratorService _service;
        private readonly MaintainController _controller;

        public ModeratorControlerTest()
        {
            ConfigureMappings();
            this._service = new ModeratorService();
            this._controller = new MaintainController(this._service);
        }

        [TestMethod]
        public void MaintainSubmitReport_ShouldRenderDefaultViewWithValidCommentId()
        {
            _controller.WithCallTo(pc => pc.SubmitReport(3))
                    .ShouldRenderDefaultView();
        }

        [TestMethod]
        public void MaintainSubmitReport_ShouldRedirectForInvalidId()
        {
            _controller.WithCallTo(pc => pc.SubmitReport(-4))
                .ShouldRedirectTo<ProductsController>(pc => pc.All());
        }

        [TestMethod]
        public void MaintainSubmitReport_ShouldRenderDefaultViewWithProperViewModel()
        {
            _controller.WithCallTo(pc => pc.SubmitReport(3))
                    .ShouldRenderDefaultView()
                    .WithModel<SubmitReportViewModel>();
        }

        [TestMethod]
        public void MaintainEvaluateReports_ShouldRenderDefaultView()
        {
            _controller.WithCallTo(pc => pc.EvaluateReports())
                    .ShouldRenderDefaultView();
        }

        [TestMethod]
        public void MaintainEvaluateReports_ShouldRenderDefaultViewWithProperModel()
        {
            _controller.WithCallTo(pc => pc.EvaluateReports())
                    .ShouldRenderDefaultView()
                    .WithModel<IEnumerable<EvaluateReportViewModel>>();
        }

        [TestMethod]
        public void MaintainDismissReport_ShouldReturnBadRequestErrorForInvalidId()
        {
            _controller.WithCallTo(pc => pc.DismissReport(1999999))
                .ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void MaintainIssueWarning_ShouldReturnBadRequestErrorForInvalidId()
        {
            _controller.WithCallTo(pc => pc.IssueWarning(1999999))
                .ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void MaintainJudgeWarnings_ShouldRenderDefaultView()
        {
            _controller.WithCallTo(pc => pc.JudgeWarnings())
                    .ShouldRenderDefaultView();
        }

        [TestMethod]
        public void MaintainJudgeWarnings_ShouldRenderDefaultViewWithProperViewModel()
        {
            _controller.WithCallTo(pc => pc.JudgeWarnings())
                    .ShouldRenderDefaultView()
                    .WithModel<IEnumerable<UserWarningsViewModel>>();
        }

        private void ConfigureMappings()
        {
            Mapper.Initialize(m =>
            {
                m.CreateMap<IdentityRole, RoleViewModel>();
                m.CreateMap<Product, ManageProductViewModel>();
                m.CreateMap<Product, EditProductViewModel>(); m.CreateMap<Report, EvaluateReportViewModel>()
             .ForMember(ervm => ervm.CommentContent, expr => expr.MapFrom(r => r.OffensiveComment.Content))
             .ForMember(ervm => ervm.SubmittedBy, expr => expr.MapFrom(r => r.Snitch.User.UserName))
             .ForMember(ervm => ervm.CommentOffender, expr => expr.MapFrom(r => r.OffensiveComment.Customer.User.UserName))
             .ForMember(ervm => ervm.ReviewId, expr => expr.MapFrom(r => r.OffensiveComment.Review.Id));
            });
        }
    }
}