using AsyncNet.Selenium.Common.Configuration;
using AsyncNet.TestJobs;

namespace AsyncNet.Selenium.Common
{
    public class SeleniumBeforeActionContext
    {
        private readonly IBeforeActionContext beforeActionContext;
        private readonly ISeleniumSettings settings;

        public string SessionId => beforeActionContext.SessionId;

        public string TestId => beforeActionContext.TestId;

        public SeleniumBeforeActionContext(
            IBeforeActionContext beforeActionContext,
            ISeleniumSettings settings)
        {
            this.beforeActionContext = beforeActionContext;
            this.settings = settings;
        }
    }
}
