using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;

namespace WebDriverTests
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
            OurAppMustBeAbleToRun(new string[0]);
        }

        [TestMethod]
        private void OurAppMustBeAbleToRun(string [] args)
        {
            
        }

        [TestMethod]
        public void VerifyPageTitle()
        {
            // Replace with your own test logic
            _driver.Url = "https://www.bing.com";
            if(!_driver.Title .Contains( "±ÿ”¶"))
            {
                throw new Exception("");
            }
        }

        [TestCleanup]
        public void EdgeDriverCleanup()
        {
            _driver.Quit();
        }
    }
}
