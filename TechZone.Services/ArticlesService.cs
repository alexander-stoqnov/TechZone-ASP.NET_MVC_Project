namespace TechZone.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Models.ViewModels.Articles;
    using System;
    using Models.EntityModels;

    public class ArticlesService : Service
    {
        public void AddArticle(string currentUserId, AddArticleViewModel aavm)
        {
            Customer customer = this.Context.Customers.First(c => c.User.Id == currentUserId);
            Article article = Mapper.Instance.Map<Article>(aavm);
            article.PublishDate = DateTime.Now;
            article.Publisher = customer;
            this.Context.Articles.Add(article);
            this.Context.SaveChanges();
        }

        public IEnumerable<GeneralArticleViewModel> GetAllArticles(string authorName)
        {
            var articles = this.Context.Articles.Where(a => a.Publisher.User.UserName.Contains(authorName)).OrderByDescending(a => a.PublishDate);
            return Mapper.Map<IEnumerable<GeneralArticleViewModel>>(articles);
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

        //public void EditSelectedArticle(EditArticleViewModel eavm)
        //{
        //    Article article = this.Context.Articles.Find(eavm.Id);
        //    article.Content = eavm.Content;
        //    article.Title = eavm.Title;
        //    this.Context.SaveChanges();
        //}

        public void DeleteArticle(int id)
        {
            Article article = this.Context.Articles.Find(id);
            this.Context.Articles.Remove(article);
            this.Context.SaveChanges();
        }
    }
}