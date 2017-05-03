namespace TechZone.Web.Tests.Automation
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Support.UI;
    using Helpers;
    using System.Linq;
    using System.Threading;
    using Data;

    /// <summary>
    /// These test the functionality for new users to add reviews
    /// </summary>
    [TestClass]
    public class ReviewsTests
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private string _validUsername;
        private string _validPassword;
        private Random rnd;
        private TechZoneContext context;

        public ReviewsTests()
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
            _wait.Until(ExpectedConditions.ElementExists(By.Id("registerLink")));
            _driver.FindElement(By.Id("registerLink")).Click();
        }

        [TestMethod]
        public void NewlyRegisteredUsersShouldBeAbleToPostReviews()
        {
            // Registering a new Customer
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

            // Selecting the first product in the database
            int firstProductId = this.context.Products.First().Id;
            _wait.Until(ExpectedConditions.ElementExists(By.XPath($"//*[@id=\"{firstProductId}\"]/div/div[2]/div[2]/p/a"))).Click();
            _wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id=\"myTab\"]/li[3]/a"))).Click();

            // Clicking the Submit Review button
            Thread.Sleep(1000);
            _wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id=\"open-review-box\"]"))).Click();

            string oldRating = _wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id=\"service-three\"]/div[2]/div[1]/div[1]/div/h2"))).Text.Trim();

            // Filling the review information - title, content, random rating
            string testTitle = "Jibber jabber random review Title";
            var titleBox = _wait.Until(ExpectedConditions.ElementExists(By.Id("title")));
            foreach (var letter in testTitle)
            {
                titleBox.SendKeys(letter.ToString());
            }

            var contextBox = _wait.Until(ExpectedConditions.ElementExists(By.Id("new-review")));
            string testContent = "Jibber jabber random review Content, blah blah blah, some boring useless test. Maybe i should create a nuget package for generating lorem ipsum content. But its probably already done. Enough symbols for now. Time to select a rating";
            foreach (var letter in testContent)
            {
                contextBox.SendKeys(letter.ToString());
            }
            int ratingToGive = rnd.Next(1, 6);
            _wait.Until(ExpectedConditions.ElementExists(By.XPath($"//*[@id=\"post-review-box\"]/div/form/div[4]/div/span[{ratingToGive}]"))).Click();

            _wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id=\"post-review-box\"]/div/form/div[4]/button"))).Click();
            
            Thread.Sleep(1000);
            _wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id=\"myTab\"]/li[3]/a"))).Click();

            // After submitting review, check the new rating of the product.
            string newRating = _wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id=\"service-three\"]/div[2]/div[1]/div[1]/div/h2"))).Text.Trim();

            // Making sure you get the information that you already wrote a review for that product.
            Thread.Sleep(1000);
            string expectedMessageIfYouPostedReview =
                "You have already submitted a review! Click Here to read your review.";
            string actualMessage = _wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id=\"service-three\"]/div[1]/div/div/div"))).Text.Trim();

            Assert.AreEqual(expectedMessageIfYouPostedReview, actualMessage);
            Assert.AreNotEqual(oldRating, newRating);
        }
    }
}