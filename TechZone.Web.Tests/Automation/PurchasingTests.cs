namespace TechZone.Web.Tests.Automation
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Support.UI;
    using System.Threading;

    /// <summary>
    /// These tests are for the functionality of purchasing items
    /// </summary>
    [TestClass]
    public class PurchasingTests
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public PurchasingTests()
        {
            this._driver = new ChromeDriver();
            this._wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
        }

        [TestInitialize]
        public void Init()
        {
            // Arrange
            _driver.Navigate().GoToUrl("http://localhost:15777/");
            _driver.Manage().Window.Maximize();
            _wait.Until(ExpectedConditions.ElementExists(By.XPath("/html/body/div[1]/div/div[2]/ul[1]/li[2]/a"))).Click();
            _wait.Until(ExpectedConditions.ElementExists(By.XPath("/html/body/div[1]/div/div[2]/ul[1]/li[2]/ul/li[1]/a"))).Click();
        }

        //[TestMethod]
        public void Purchase_ShouldAllowGuestsToAddItemsToTheirCart()
        {
            _wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id=\"form1\"]/a/input"))).Click();
            _wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id=\"form2\"]/a/input"))).Click();
            _wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id=\"form3\"]/a/input"))).Click();

            Thread.Sleep(1000);
            _wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id=\"shopping-cart\"]/a"))).Click();

            _wait.Until(ExpectedConditions.ElementExists(By.XPath("/html/body/div[2]/div/div/div/table/tbody")));
            var itemsInCheckout = _driver.FindElements(By.XPath("/html/body/div[2]/div/div/div/table/tbody/tr")).Count;

            // 3 products that were added + 3 lines with price information and shipment.
            Assert.AreEqual(6, itemsInCheckout);
            // _driver.Quit();
        }
        [TestMethod]
        public void Purchase_ClickingAddToCartMultipleTimesForSingleProductShouldAddItOnlyOnceToCart()
        {
            _wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id=\"form1\"]/a/input"))).Click();
            _wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id=\"form2\"]/a/input"))).Click();
            _wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id=\"form3\"]/a/input"))).Click();
            _wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id=\"form2\"]/a/input"))).Click();
            _wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id=\"form1\"]/a/input"))).Click();

            Thread.Sleep(1000);
            _wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id=\"shopping-cart\"]/a"))).Click();

            _wait.Until(ExpectedConditions.ElementExists(By.XPath("/html/body/div[2]/div/div/div/table/tbody")));
            var itemsInCheckout = _driver.FindElements(By.XPath("/html/body/div[2]/div/div/div/table/tbody/tr")).Count;

            // 3 products that were added  (5 were clicked) + 3 lines with price information and shipment.
            Assert.AreEqual(6, itemsInCheckout);
            _driver.Quit();
        }
    }
}