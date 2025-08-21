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

			// ��ȡ CSS ����ֵ
			string bgColor = button.GetCssValue("background-color");
			Console.WriteLine($"��ť����ɫ: {bgColor}");
		}

		[TestCleanup]
		public void EdgeDriverCleanup()
		{
			_driver.Quit();
		}
	}
}

#endif

