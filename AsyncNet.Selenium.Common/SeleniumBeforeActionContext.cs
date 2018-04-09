using AsyncNet.TestJobs;

namespace AsyncNet.Selenium.Common
{
    public class SeleniumBeforeActionContext
    {
        private readonly IBeforeActionContext beforeActionContext;

        public SeleniumBeforeActionContext(IBeforeActionContext beforeActionContext)
        {
            this.beforeActionContext = beforeActionContext;
        }
    }
}
