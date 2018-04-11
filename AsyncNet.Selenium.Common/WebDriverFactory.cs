using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AsyncNet.TestJobs
{
    public class WebDriverFactory
    {
        public enum Browser
        {
            None,
            Chrome
        }

        public IWebDriver GetDriver(Browser browser)
        {
            switch(browser)
            {
                case Browser.Chrome:
                    return new ChromeDriver("C:\\CHANDA\\TEST_APPS\\AsyncNet_Repo\\AsyncNet.Start\\bin\\Debug\\netcoreapp2.0");
                default:
                    throw new ApplicationException("Unable to find web driver");
            }
        }
    }
}