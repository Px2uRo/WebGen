#if EGDRV
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;


namespace WebGenEdgeDriveTests
{
	[Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
	public class MyTestClass
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
		public void MyCSSValueShouldBe()
		{
			var button = _driver.FindElement(By.CssSelector("button"));

			// 获取 CSS 属性值
			string bgColor = button.GetCssValue("background-color");
			Console.WriteLine($"按钮背景色: {bgColor}");
		}

		[TestCleanup]
		public void EdgeDriverCleanup()
		{
			_driver.Quit();
		}
	}
}

#endif

