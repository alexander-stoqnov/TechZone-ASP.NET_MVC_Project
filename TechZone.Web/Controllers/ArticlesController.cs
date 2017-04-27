namespace TechZone.Web.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Models.ViewModels.Articles;
    using Services;
    using Attributes;
    using Microsoft.AspNet.Identity;

    [RoutePrefix("Articles")]
    public class ArticlesController : Controller
    {
        private ArticlesService _service;

        public ArticlesController()
        {
            this._service = new ArticlesService();
        }

        [Route("All/{authorName=}")]
        public ActionResult All(string authorName = "")
        {
            IEnumerable<GeneralArticleViewModel> articleVms = this._service.GetAllArticles(authorName);
            return View(articleVms);
        }

        [Route("Add")]
        [CustomAuthorize(Roles = "Publisher")]
        public ActionResult Add()
        {
            return this.View();
        }

        [Route("Add")]
        [HttpPost]
        [CustomAuthorize(Roles = "Publisher")]
        public ActionResult Add(AddArticleViewModel aavm)
        {
            var currentUserId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                this._service.AddArticle(currentUserId, aavm);
                return RedirectToAction("All", "Articles");
            }

            return this.View(aavm);
        }

        [Route("Search/{content=}")]
        [HttpPost]
        public ActionResult Search(string content = "")
        {
            IEnumerable<GeneralArticleViewModel> articleVms = _service.GetFilteredArticles(content);
            return this.View("All", articleVms);
        }

        [CustomAuthorize(Roles = "Admin")]
        [Route("EditArticle/{id}")]
        public ActionResult EditArticle(int id)
        {
            EditArticleViewModel eavm = this._service.GetArticleToManage(id);
            return this.View(eavm);
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpPost]
        [Route("EditArticle/{id}")]
        public ActionResult EditArticle(EditArticleViewModel eavm)
        {
            if (ModelState.IsValid)
            {
                this._service.EditSelectedArticle(eavm);
                return RedirectToAction("All", "Articles", new { area = "Blog" });
            }

            return this.View(eavm);
        }

        [CustomAuthorize(Roles = "Admin")]
        [Route("DeleteArticle/{id}")]
        public ActionResult DeleteArticle(int id)
        {
            EditArticleViewModel eavm = _service.GetArticleToManage(id);
            return this.View(eavm);
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpPost]
        [Route("DeleteArticle/{id}")]
        public ActionResult DeleteArticle(EditArticleViewModel eavm)
        {
            this._service.DeleteArticle(eavm.Id);
            return RedirectToAction("All", "Articles", new { area = "Blog" });
        }
    }
}