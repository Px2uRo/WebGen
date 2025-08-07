using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace WebGen.EdgeDriverTest
{
    [TestClass]
    public class EdgeDriverTest
    {
        // In order to run the below test(s), 
        // please follow the instructions from https://docs.microsoft.com/en-us/microsoft-edge/webdriver-chromium
        // to install Microsoft Edge WebDriver.

        private EdgeDriver _driver;

        [TestInitialize]
        public void EdgeDriverInitialize()
        {
            // Initialize edge driver 
            var options = new EdgeOptions
            {
                PageLoadStrategy = PageLoadStrategy.Normal
            };
            _driver = new EdgeDriver(options);
        }

        [TestMethod]
        public void StartHost(string[] args)
        {
            WebGen.MinimalAPI.Program.Main(args);
        }

        [TestMethod]
        public void VerifyPageTitle()
        {
            
            _driver.Navigate().GoToUrl("https://cn.bing.com");
            if (!_driver.Title.Contains("±ÿ”¶"))
            {
                throw new InternalTestFailureException();
            }
        }

        [TestCleanup]
        public void EdgeDriverCleanup()
        {
            _driver.Quit();
        }
    }
}
