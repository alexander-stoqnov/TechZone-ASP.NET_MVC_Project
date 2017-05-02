namespace TechZone.Web.Tests.Automation
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Support.UI;
    using System.Text;

    /// <summary>
    /// These test are for the user accounts registration and login. Testing both valid and invalid data.
    /// </summary>
    [TestClass]
    public class NewUserRegistrationTests
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public NewUserRegistrationTests()
        {
            this.driver = new ChromeDriver();
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [TestInitialize]
        public void Init()
        {
            // Arrange
            driver.Navigate().GoToUrl("http://localhost:1574/");
            driver.Manage().Window.Maximize();
            wait.Until(ExpectedConditions.ElementExists(By.Id("registerLink")));
            driver.FindElement(By.Id("registerLink")).Click();
        }

        //[TestMethod]
        public void Register_ShouldBeAbleToRegisterWithValidUsernamePassword()
        {
            // Act
            string validUsername = this.RandomUsernameGenerator();
            string validPassword = this.RandomPasswordGenerator();

            wait.Until(ExpectedConditions.ElementExists(By.Id("Username")));
            driver.FindElement(By.Id("Username")).SendKeys(validUsername);
            driver.FindElement(By.Id("Password")).SendKeys(validPassword);
            driver.FindElement(By.Id("ConfirmPassword")).SendKeys(validPassword);
            driver.FindElement(By.Id("Email")).SendKeys($"{validUsername}@email.com");
            driver.FindElement(By.Id("FirstName")).SendKeys("UnitTestFirst");
            driver.FindElement(By.Id("LastName")).SendKeys("UnitTestLast");
            driver.FindElement(By.ClassName("login-button")).Click();

            wait.Until(ExpectedConditions.ElementExists(By.Id("username")));

            string expectedText = validUsername;
            string actualText = driver.FindElement(By.Id("username")).Text;

            // Assert
            Assert.AreEqual(expectedText, actualText);
            driver.Quit();
        }

        //[TestMethod]
        public void Register_ShouldShowAnErrorIfPasswordsAreDifferent()
        {
            // Act
            string validUsername = this.RandomUsernameGenerator();

            wait.Until(ExpectedConditions.ElementExists(By.Id("Username")));
            driver.FindElement(By.Id("Username")).SendKeys(validUsername);
            driver.FindElement(By.Id("Password")).SendKeys("password1!");
            driver.FindElement(By.Id("ConfirmPassword")).SendKeys("password2!");
            driver.FindElement(By.Id("Email")).SendKeys($"{validUsername}@email.com");
            driver.FindElement(By.Id("FirstName")).SendKeys("UnitTestFirst");
            driver.FindElement(By.Id("LastName")).SendKeys("UnitTestLast");
            driver.FindElement(By.ClassName("login-button")).Click();

            wait.Until(ExpectedConditions.ElementExists(By.ClassName("validation-summary-errors")));

            string expectedErrorMessage = "The password and confirmation password do not match.";
            string actualErrorMessage = driver.FindElement(By.XPath("//*[@id=\"register-form\"]/div[2]/div[2]/ul/li")).Text;
            
            // Assert
            Assert.AreEqual(expectedErrorMessage, actualErrorMessage);
            driver.Quit();
        }

        //[TestMethod]
        public void Register_ShouldShowAnErrorIfPasswordDoesNotMatchPattern()
        {
            // Act
            string validUsername = this.RandomUsernameGenerator();

            wait.Until(ExpectedConditions.ElementExists(By.Id("Username")));
            driver.FindElement(By.Id("Username")).SendKeys(validUsername);
            driver.FindElement(By.Id("Password")).SendKeys("password");
            driver.FindElement(By.Id("ConfirmPassword")).SendKeys("password");
            driver.FindElement(By.Id("Email")).SendKeys($"{validUsername}@email.com");
            driver.FindElement(By.Id("FirstName")).SendKeys("UnitTestFirst");
            driver.FindElement(By.Id("LastName")).SendKeys("UnitTestLast");
            driver.FindElement(By.ClassName("login-button")).Click();

            wait.Until(ExpectedConditions.ElementExists(By.ClassName("validation-summary-errors")));

            string expectedErrorMessage = "Passwords must have at least one digit ('0'-'9').";
            string actualErrorMessage = driver.FindElement(By.XPath("//*[@id=\"register-form\"]/div[2]/div[2]/ul/li")).Text;

            // Assert
            Assert.AreEqual(expectedErrorMessage, actualErrorMessage);
            driver.Quit();
        }

        [TestMethod]
        public void Register_ShouldReturnAWarningForTakenEmailWithoutClickingRegisterButton()
        {
            // Act
            string validUsername = this.RandomUsernameGenerator();

            wait.Until(ExpectedConditions.ElementExists(By.Id("Username")));
            driver.FindElement(By.Id("Username")).SendKeys(validUsername);
            driver.FindElement(By.Id("Password")).SendKeys("password");
            driver.FindElement(By.Id("ConfirmPassword")).SendKeys("password");
            driver.FindElement(By.Id("Email")).SendKeys("pesho@g.c");

            wait.Until(ExpectedConditions.ElementExists(By.Id("email-warning")));

            string expectedErrorMessage = "Email 'pesho@g.c' is already taken";
            string actualErrorMessage = driver.FindElement(By.Id("email-warning")).Text;

            // Assert
            Assert.AreEqual(expectedErrorMessage, actualErrorMessage);
            driver.Quit();
        }


        private string RandomUsernameGenerator()
        {
            string letters = "abcdefghijklmnopqrstuvwyzABCDEFGHIJKLMNOPQRSTUVWYZ";
            Random rnd = new Random();
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < 10; i++)
            {
                sb.Append(letters[rnd.Next(0, 42)]);
            }

            return sb.ToString();
        }

        private string RandomPasswordGenerator()
        {
            string smallLetters = "abcdefghijklmnopqrstuvwyz";
            string bigLetters = "ABCDEFGHIJKLMNOPQRSTUVWYZ";
            string digits = "0123456789";
            string specialCharacters = "!?@#$%^&*(_)[]{}";
            Random rnd = new Random();
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < 6; i++)
            {
                sb.Append(smallLetters[rnd.Next(0, 24)]);
            }
            sb.Append(bigLetters[rnd.Next(0, 23)]);
            sb.Append(digits[rnd.Next(0, 9)]);
            sb.Append(specialCharacters[rnd.Next(0, 15)]);

            return sb.ToString();
        }
    }
}
