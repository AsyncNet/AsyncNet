using AsyncNet.TestJobs;

namespace AsyncNet.Selenium.Common
{
    public class SeleniumAfterActionContext
    {
        private readonly IAfterActionContext afterActionContext;

        public SeleniumAfterActionContext(IAfterActionContext afterActionContext)
        {
            this.afterActionContext = afterActionContext;
        }
    }
}
