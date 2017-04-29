﻿using TechZone.Services.Contracts;

namespace TechZone.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using System;
    using Dropbox.Api;
    using Models.ViewModels.Articles;
    using Models.EntityModels;
    using Models.ViewModels.Home;

    public class ArticlesService : Service, IArticlesService
    {
        public void AddArticle(string currentUserId, AddArticleViewModel aavm, string fileName, byte[] file, string dropboxKey)
        {
            Customer customer = this.Context.Customers.First(c => c.User.Id == currentUserId);
            Article article = Mapper.Instance.Map<Article>(aavm);
            article.PublishDate = DateTime.Now;
            article.Publisher = customer;
            string newFileName = new Random().Next() + "_" + fileName;
            article.ImageFileName = newFileName;
            this.Context.Articles.Add(article);
            this.Context.SaveChanges();
            Upload(new DropboxClient(dropboxKey), $"/Articles/{customer.User.UserName}", newFileName, file);
        }

        public IEnumerable<GeneralArticleViewModel> GetAllArticles(string publisherName, string dropboxKey)
        {
            var articles = this.Context.Articles
                .Where(a => a.Publisher.User.UserName.Contains(publisherName))
                .OrderByDescending(a => a.PublishDate).ToList();
            var articlesVms = new HashSet<GeneralArticleViewModel>();
            foreach (var article in articles)
            {
                var articleVm = Mapper.Instance.Map<GeneralArticleViewModel>(article);
                articleVm.ContentParagraphs = article.Content.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                if (article.ImageFileName != null)
                {
                    articleVm.ImageData = this.DownloadArticlePicture(article.ImageFileName, article.Publisher.User.UserName,
      dropboxKey);                  
                }
                articlesVms.Add(articleVm);
            }

            return articlesVms;
        }

        private string DownloadArticlePicture(string articleFileName, string publisherUsername, string dropboxKey)
        {
            var imageByteData = this.DownloadAsync(new DropboxClient(dropboxKey), $"Articles/{publisherUsername}", articleFileName);
            string imageBase64Data = Convert.ToBase64String(imageByteData.Result);
            return $"data:image/*;base64,{imageBase64Data}";
        }

        public IEnumerable<GeneralArticleViewModel> GetFilteredArticles(string content)
        {
            var articles = this.Context.Articles.Where(a => a.Title.Contains(content) || a.Content.Contains(content)).OrderByDescending(a => a.PublishDate);
            return Mapper.Map<IEnumerable<GeneralArticleViewModel>>(articles);
        }

        public EditArticleViewModel GetArticleToManage(int id)
        {
            Article article = this.Context.Articles.Find(id);
            return Mapper.Map<EditArticleViewModel>(article);
        }

        public void EditSelectedArticle(EditArticleViewModel eavm)
        {
            Article article = this.Context.Articles.Find(eavm.Id);
            article.Content = eavm.Content;
            article.Title = eavm.Title;
            this.Context.SaveChanges();
        }

        public void DeleteArticle(int id)
        {
            Article article = this.Context.Articles.Find(id);
            this.Context.Articles.Remove(article);
            this.Context.SaveChanges();
        }

        public ICollection<LatestArticleViewModel> GetHomePageLatestArticles(string dropboxKey)
        {
            var articles = this.Context.Articles.OrderByDescending(a => a.PublishDate).Take(3).ToList();
            ICollection<LatestArticleViewModel> articleVms = new List<LatestArticleViewModel>();
            foreach (var article in articles)
            {
                LatestArticleViewModel articleVm = Mapper.Instance.Map<LatestArticleViewModel>(article);
                articleVm.ContentParagraphs = article.Content.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                if (article.ImageFileName != null)
                {
                    articleVm.ImageData = DownloadArticlePicture(article.ImageFileName, article.Publisher.User.UserName,
                        dropboxKey);
                }
                articleVms.Add(articleVm);
            }
            return articleVms;
        }
    }
}