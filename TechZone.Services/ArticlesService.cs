namespace TechZone.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Models.ViewModels.Articles;
    using System;
    using Models.EntityModels;
    using Dropbox.Api;

    public class ArticlesService : Service
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
                if (article.ImageFileName != null)
                {
                    try
                    {
                        articleVm.ImageData = this.DownloadArticlePicture(article.ImageFileName, article.Publisher.User.UserName,
      dropboxKey);
                    }
                    catch (Exception)
                    {
                        throw new InvalidOperationException();
                    }                    
                }
                articlesVms.Add(articleVm);
            }

            return articlesVms;
        }

        private string DownloadArticlePicture(string articleFileName, string publisherUsername, string dropboxKey)
        {
            try
            {
                var imageByteData = this.DownloadAsync(new DropboxClient(dropboxKey), $"Articles/{publisherUsername}", articleFileName);
                string imageBase64Data = Convert.ToBase64String(imageByteData.Result);
                return $"data:image/*;base64,{imageBase64Data}";
            }
            catch (Exception)
            {
                throw new InvalidOperationException();
            }

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
    }
}