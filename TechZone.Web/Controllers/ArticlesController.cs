namespace TechZone.Web.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Models.ViewModels.Articles;
    using Services;
    using Attributes;
    using Microsoft.AspNet.Identity;
    using System.Web;
    using System.IO;
    using System;

    [RoutePrefix("Articles")]
    public class ArticlesController : Controller
    {
        private ArticlesService _service;

        public ArticlesController()
        {
            this._service = new ArticlesService();
        }

        [Route("All/{authorName=}")]
        [HandleError(ExceptionType = typeof(ArgumentException), View = "WaitForDownload")]
        public ActionResult All(string authorName = "")
        {
            var apikey = System.IO.File.ReadAllLines(Server.MapPath("~/Scripts/CustomScripts/") + "keys.txt");
            try
            {
                IEnumerable<GeneralArticleViewModel> articleVms = this._service.GetAllArticles(authorName, apikey[1]);
                return View(articleVms);
            }
            catch (Exception)
            {
                throw new ArgumentException();
            }
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
        [HandleError(ExceptionType = typeof(HttpRequestValidationException), View = "NaughtyStringsError")]
        public ActionResult Add(AddArticleViewModel aavm)
        {
            var apikey = System.IO.File.ReadAllLines(Server.MapPath("~/Scripts/CustomScripts/") + "keys.txt");
            HttpPostedFileBase file = this.Request.Files["articlePicture"];

            if (file == null)
            {
                ModelState.AddModelError("", "Please browse for a valid image file, and then click Upload.");
                return this.View(aavm);
            }

            if (file.ContentLength > 2097152)
            {
                ModelState.AddModelError("", "Image size should be less than 2mb.");
                return this.View(aavm);
            }

            string fileName = Path.GetFileName(file.FileName);
            if (!fileName.ToLower().EndsWith(".jpg") && !fileName.ToLower().EndsWith(".png") && !fileName.ToLower().EndsWith(".jpeg"))
            {
                ModelState.AddModelError("", "Invalid picture format!");
                return this.View(aavm);
            }

            var currentUserId = User.Identity.GetUserId();
            MemoryStream memstr = new MemoryStream();
            file.InputStream.CopyTo(memstr);
            byte[] imageData = memstr.ToArray();
            if (ModelState.IsValid)
            {
                this._service.AddArticle(currentUserId, aavm, fileName, imageData, apikey[1]);
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
                return RedirectToAction("All", "Articles");
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
            return RedirectToAction("All", "Articles");
        }
    }
}