using AsyncNet.Selenium.Common.Configuration;
using AsyncNet.TestJobs;

namespace AsyncNet.Selenium.Common
{
    public abstract class SeleniumTest : TestCase
    {
        private readonly SeleniumSettingsFactory settingsFactory;

        public SeleniumTest()
        {
            var factory = new SeleniumSettingsFactory();
        }

        protected override void Before(IBeforeActionContext context)
        {
            base.Before(context);
            Before(new SeleniumBeforeActionContext(context, null));
        }

        protected override void Execute(IActionContext context)
        {
            base.Execute(context);
            Execute(new SeleniumActionContext(context));
        }

        protected override void After(IAfterActionContext context)
        {
            base.After(context);
            After(new SeleniumAfterActionContext(context));
        }

        protected virtual void Before(SeleniumBeforeActionContext context)
        {

        }

        protected virtual void After(SeleniumAfterActionContext context)
        {

        }

        protected virtual void Execute(SeleniumActionContext context)
        {

        }
    }
}
