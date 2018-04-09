using AsyncNet.Selenium.Common;
using System.Threading;

namespace AsyncNet.Selenium.Tests
{
    public class TestCaseFour : SeleniumTest
    {
        private readonly TestCaseOne testCaseOne;

        public TestCaseFour(TestCaseOne testCaseOne)
        {
            this.testCaseOne = testCaseOne;
        }

        protected override void Execute(SeleniumActionContext context)
        {
            Thread.Sleep(6000);
        }

        protected override void After(SeleniumAfterActionContext context)
        {
            Thread.Sleep(4000);
        }
    }
}
