using AsyncNet.TestJobs;

namespace AsyncNet.Selenium.Common
{
    public class SeleniumActionContext
    {
        private readonly IActionContext actionContext;

        public SeleniumActionContext(IActionContext actionContext)
        {
            this.actionContext = actionContext;
        }
    }
}
