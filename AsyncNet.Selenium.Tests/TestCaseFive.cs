using AsyncNet.Selenium.Common;
using System.Threading;

namespace AsyncNet.Selenium.Tests
{
    public class TestCaseFive : SeleniumTest
    {
        private readonly TestCaseFour testCaseFour;

        public TestCaseFive(TestCaseFour testCaseFour)
        {
            this.testCaseFour = testCaseFour;
        }

        protected override void Execute(SeleniumActionContext context)
        {
            Thread.Sleep(1000);
        }

        protected override void After(SeleniumAfterActionContext context)
        {
            Thread.Sleep(1000);
        }
    }
}
