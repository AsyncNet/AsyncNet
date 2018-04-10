using AsyncNet.Selenium.Common;
using AsyncNet.TestJobs;
using OpenQA.Selenium;
using System.Threading;
using static AsyncNet.TestJobs.WebDriverFactory;

namespace AsyncNet.Selenium.Tests
{
    public class TestCaseOne : SeleniumTest
    {
        public string Name { get; protected set; }

        protected override void Execute(SeleniumActionContext context)
        {
            var factory = new WebDriverFactory();


            using(IWebDriver driver = factory.GetDriver(Browser.Chrome))
            {
                driver.Url = "http://google.com";
                Thread.Sleep(1000);
                Name = "First test";
            }
        }

        protected override void After(SeleniumAfterActionContext context)
        {
            Thread.Sleep(2000);
        }
    }
}
