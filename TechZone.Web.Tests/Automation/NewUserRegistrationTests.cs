namespace TechZone.Web.Tests.Automation
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Support.UI;
    using Helpers;

    /// <summary>
    /// These test are for the user accounts registration and login. Testing both valid and invalid data.
    /// </summary>
    [TestClass]
    public class NewUserRegistrationTests
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private string _validUsername;
        private string _validPassword;

        public NewUserRegistrationTests()
        {
            this._driver = new ChromeDriver();
            this._wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        [TestInitialize]
        public void Init()
        {
            // Arrange
            _validUsername = TextGenerator.RandomUsernameGenerator();
            _validPassword = TextGenerator.RandomPasswordGenerator();

            _driver.Navigate().GoToUrl("http://localhost:1574/");
            _driver.Manage().Window.Maximize();
            _wait.Until(ExpectedConditions.ElementExists(By.Id("registerLink")));
            _driver.FindElement(By.Id("registerLink")).Click();
        }

        [TestMethod]
        public void Register_ShouldBeAbleToRegisterWithValidUsernamePassword()
        {
            // Act
            _wait.Until(ExpectedConditions.ElementExists(By.Id("Username")));
            _driver.FindElement(By.Id("Username")).SendKeys(_validUsername);
            _driver.FindElement(By.Id("Password")).SendKeys(_validPassword);
            _driver.FindElement(By.Id("ConfirmPassword")).SendKeys(_validPassword);
            _driver.FindElement(By.Id("Email")).SendKeys($"{_validUsername}@email.com");
            _driver.FindElement(By.Id("FirstName")).SendKeys("UnitTestFirst");
            _driver.FindElement(By.Id("LastName")).SendKeys("UnitTestLast");
            _driver.FindElement(By.ClassName("login-button")).Click();

            _wait.Until(ExpectedConditions.ElementExists(By.Id("username")));

            string expectedText = _validUsername;
            string actualText = _driver.FindElement(By.Id("username")).Text;

            // Assert
            Assert.AreEqual(expectedText, actualText);
            _driver.Quit();
        }

        [TestMethod]
        public void Register_ShouldShowAnErrorIfPasswordsAreDifferent()
        {
            // Act
            _wait.Until(ExpectedConditions.ElementExists(By.Id("Username")));
            _driver.FindElement(By.Id("Username")).SendKeys(_validUsername);
            _driver.FindElement(By.Id("Password")).SendKeys("password1!");
            _driver.FindElement(By.Id("ConfirmPassword")).SendKeys("password2!");
            _driver.FindElement(By.Id("Email")).SendKeys($"{_validUsername}@email.com");
            _driver.FindElement(By.Id("FirstName")).SendKeys("UnitTestFirst");
            _driver.FindElement(By.Id("LastName")).SendKeys("UnitTestLast");
            _driver.FindElement(By.ClassName("login-button")).Click();

            _wait.Until(ExpectedConditions.ElementExists(By.ClassName("validation-summary-errors")));

            string expectedErrorMessage = "The password and confirmation password do not match.";
            string actualErrorMessage = _driver.FindElement(By.XPath("//*[@id=\"register-form\"]/div[2]/div[2]/ul/li")).Text;
            
            // Assert
            Assert.AreEqual(expectedErrorMessage, actualErrorMessage);
            _driver.Quit();
        }

        [TestMethod]
        public void Register_ShouldShowAnErrorIfPasswordDoesNotMatchPattern()
        {
            // Act
            _wait.Until(ExpectedConditions.ElementExists(By.Id("Username")));
            _driver.FindElement(By.Id("Username")).SendKeys(_validUsername);
            _driver.FindElement(By.Id("Password")).SendKeys("password");
            _driver.FindElement(By.Id("ConfirmPassword")).SendKeys("password");
            _driver.FindElement(By.Id("Email")).SendKeys($"{_validUsername}@email.com");
            _driver.FindElement(By.Id("FirstName")).SendKeys("UnitTestFirst");
            _driver.FindElement(By.Id("LastName")).SendKeys("UnitTestLast");
            _driver.FindElement(By.ClassName("login-button")).Click();

            _wait.Until(ExpectedConditions.ElementExists(By.ClassName("validation-summary-errors")));

            string expectedErrorMessage = "Passwords must have at least one digit ('0'-'9').";
            string actualErrorMessage = _driver.FindElement(By.XPath("//*[@id=\"register-form\"]/div[2]/div[2]/ul/li")).Text;

            // Assert
            Assert.AreEqual(expectedErrorMessage, actualErrorMessage);
            _driver.Quit();
        }

        [TestMethod]
        public void Register_ShouldReturnAWarningForTakenEmailWithoutClickingRegisterButton()
        {
            // Act
            _wait.Until(ExpectedConditions.ElementExists(By.Id("Username")));
            _driver.FindElement(By.Id("Username")).SendKeys(_validUsername);
            _driver.FindElement(By.Id("Password")).SendKeys("password");
            _driver.FindElement(By.Id("ConfirmPassword")).SendKeys("password");
            _driver.FindElement(By.Id("Email")).SendKeys("pesho@g.c");

            _wait.Until(ExpectedConditions.ElementExists(By.Id("email-warning")));

            string expectedErrorMessage = "Email 'pesho@g.c' is already taken";
            string actualErrorMessage = _driver.FindElement(By.Id("email-warning")).Text;

            // Assert
            Assert.AreEqual(expectedErrorMessage, actualErrorMessage);
            _driver.Quit();
        }       
    }
}