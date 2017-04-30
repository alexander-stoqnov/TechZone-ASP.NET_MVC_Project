namespace TechZone.Services.Contracts
{
    using System.Collections.Generic;
    using Models.ViewModels.Articles;
    using Models.ViewModels.Home;

    public interface IArticlesService
    {
        void AddArticle(string currentUserId, AddArticleViewModel aavm, string fileName, byte[] file, string dropboxKey);
        IEnumerable<GeneralArticleViewModel> GetAllArticles(string publisherName, string dropboxKey);
        IEnumerable<GeneralArticleViewModel> GetFilteredArticles(string content, string dropboxKey);
        EditArticleViewModel GetArticleToManage(int id);
        void EditSelectedArticle(EditArticleViewModel eavm);
        void DeleteArticle(int id);
        ICollection<LatestArticleViewModel> GetHomePageLatestArticles(string dropboxKey);
    }
}