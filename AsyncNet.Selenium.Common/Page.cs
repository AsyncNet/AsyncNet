using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace AsyncNet.TestJobs
{
    public class Page
    {
        private readonly IWebDriver webDriver;

        public Page(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        public TPage GetPage<TPage>() where TPage : IBasePage, new()
        {
            var page = new TPage();
            var ipage = (IBasePage)page;
            ipage.Page = this;
            ipage.WebDriver = webDriver;
            return page;
        }
    }
}