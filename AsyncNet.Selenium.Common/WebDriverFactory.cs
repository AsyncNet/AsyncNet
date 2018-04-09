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
                    return new ChromeDriver();
                default:
                    throw new ApplicationException("Unable to find web driver");
            }
        }
    }
}