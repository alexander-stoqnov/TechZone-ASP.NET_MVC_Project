namespace TechZone.Services.Contracts
{
    using System.Collections.Generic;
    using Models.ViewModels.Articles;
    using Models.ViewModels.Home;

    public interface IArticlesService
    {
        void AddArticle(string currentUserId, AddArticleViewModel aavm, string fileName, byte[] file);
        IEnumerable<GeneralArticleViewModel> GetAllArticles(string publisherName);
        IEnumerable<GeneralArticleViewModel> GetFilteredArticles(string content);
        EditArticleViewModel GetArticleToManage(int? id);
        void EditSelectedArticle(EditArticleViewModel eavm);
        void DeleteArticle(int id);
        ICollection<LatestArticleViewModel> GetHomePageLatestArticles();
        bool ArticleExists(int? id);
    }
}