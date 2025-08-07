
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace WebGen.WebView2WUPTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsNotNull(App.Current);
        }
    }
}
