using OpenQA.Selenium;

namespace AsyncNet.TestJobs
{
    public class Pages
    {
        private readonly IWebDriver webDriver;

        public Pages(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        public void OpenWebDriver()
        {
            webDriver.Url = "http://google.pl";
        }

        public void CloseWebDriver()
        {
            webDriver.Close();
        }
    }
}