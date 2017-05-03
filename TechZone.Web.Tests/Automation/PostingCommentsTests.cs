namespace TechZone.Web.Tests.Automation
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Support.UI;
    using Data;
    using Helpers;
    using System.Linq;
    using System.Threading;

    /// <summary>
    /// These tests try different functions about comments for reviews.
    /// </summary>
    [TestClass]
    public class PostingCommentsTests
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private string _validUsername;
        private string _validPassword;
        private Random rnd;
        private TechZoneContext context;

        public PostingCommentsTests()
        {
            this._driver = new ChromeDriver();
            this._wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            this.context = new TechZoneContext();
            this.rnd = new Random();
        }
        [TestInitialize]
        public void Init()
        {
            _validUsername = TextGenerator.RandomUsernameGenerator();
            _validPassword = TextGenerator.RandomPasswordGenerator();

            _driver.Navigate().GoToUrl("http://localhost:15777/");
            _driver.Manage().Window.Maximize();
            //_wait.Until(ExpectedConditions.ElementExists(By.Id("registerLink")));
            //_driver.FindElement(By.Id("registerLink")).Click();
        }
        [TestMethod]
        public void AnonymousUsersShouldntBeAbleToPostCommentsToReviews()
        {
            // Clicking on the navigation panel Products => All
            _wait.Until(ExpectedConditions.ElementExists(By.XPath("/html/body/div[1]/div/div[2]/ul[1]/li[2]/a"))).Click();
            _wait.Until(ExpectedConditions.ElementExists(By.XPath("/html/body/div[1]/div/div[2]/ul[1]/li[2]/ul/li[1]/a"))).Click();

            // Selecting the first product in the database which has a review already
            int firstProductWithReviewId = this.context.Reviews.First().Product.Id;
            _wait.Until(ExpectedConditions.ElementExists(By.XPath($"//*[@id=\"{firstProductWithReviewId}\"]/div/div[2]/div[2]/p/a"))).Click();
            _wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id=\"myTab\"]/li[3]/a"))).Click();

            // Clicking the review details
            Thread.Sleep(500);
            _wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id=\"service-three\"]/div[2]/div[2]/div/div/div[1]/div[2]/div[2]/a"))).Click();

            string expectedWarning = "Please Login to post a comment!";
            string actualWarning = _wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id=\"comments\"]/p"))).Text.Trim();

            Assert.AreEqual(expectedWarning, actualWarning);
        }

        [TestMethod]
        public void RegisteredUsersShouldBeAbleToPostCommentsForExistingReviews()
        {
            _wait.Until(ExpectedConditions.ElementExists(By.Id("registerLink")));
            _driver.FindElement(By.Id("registerLink")).Click();
            _wait.Until(ExpectedConditions.ElementExists(By.Id("Username")));
            _driver.FindElement(By.Id("Username")).SendKeys(_validUsername);
            _driver.FindElement(By.Id("Password")).SendKeys(_validPassword);
            _driver.FindElement(By.Id("ConfirmPassword")).SendKeys(_validPassword);
            _driver.FindElement(By.Id("Email")).SendKeys($"{_validUsername}@email.com");
            _driver.FindElement(By.Id("FirstName")).SendKeys("UnitTestFirst");
            _driver.FindElement(By.Id("LastName")).SendKeys("UnitTestLast");
            _driver.FindElement(By.ClassName("login-button")).Click();

            // Clicking on the navigation panel Products => All
            _wait.Until(ExpectedConditions.ElementExists(By.XPath("/html/body/div[1]/div/div[2]/ul[1]/li[2]/a"))).Click();
            _wait.Until(ExpectedConditions.ElementExists(By.XPath("/html/body/div[1]/div/div[2]/ul[1]/li[2]/ul/li[1]/a"))).Click();

            // Selecting the first product in the database which has a review already
            int firstProductWithReviewId = this.context.Reviews.First().Product.Id;
            _wait.Until(ExpectedConditions.ElementExists(By.XPath($"//*[@id=\"{firstProductWithReviewId}\"]/div/div[2]/div[2]/p/a"))).Click();
            _wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id=\"myTab\"]/li[3]/a"))).Click();

            // Clicking the review details
            Thread.Sleep(500);
            _wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id=\"service-three\"]/div[2]/div[2]/div/div/div[1]/div[2]/div[2]/a"))).Click();

            // Writing a comment in the comment form
            string commentText = "This is a test comment. Delete later. By : " + this._validUsername;
            _wait.Until(ExpectedConditions.ElementExists(By.Id("Comment"))).SendKeys(commentText);
            _driver.FindElement(By.XPath("//*[@id=\"post-reply-form\"]/form/div[2]/div/button")).Click();

            // Waiting for the submitting of a comment and then expanding the comments section
            Thread.Sleep(500);
            _wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id=\"comments\"]/ul/li/div[1]/div[1]/a[2]"))).Click();

            // Comparing the last posted comment with the text we just wrote.
            Thread.Sleep(500);
            int lastCommentForReviewId = this.context.Reviews.First().Comments.Last().Id;
            var commentElement = _wait.Until(ExpectedConditions.ElementExists(By.Id($"comment-{lastCommentForReviewId}")));
            string commentActualText = commentElement.Text.Trim();

            Assert.AreEqual(commentText, commentActualText);
        }
    }
}
