using OpenQA.Selenium;

namespace AsyncNet.TestJobs
{
    public interface IBasePage
    {
        Page Page { get; set; }

        IWebDriver WebDriver { get; set; }
    }
}
