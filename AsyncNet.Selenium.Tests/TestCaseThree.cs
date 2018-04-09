using AsyncNet.Selenium.Common;
using System.Threading;

namespace AsyncNet.Selenium.Tests
{
    public class TestCaseThree : SeleniumTest
    {
        private readonly TestCaseOne testCaseOne;
        private readonly TestCaseTwo testCaseTwo;

        public TestCaseThree(TestCaseOne testCaseOne, TestCaseTwo testCaseTwo)
        {
            this.testCaseOne = testCaseOne;
            this.testCaseTwo = testCaseTwo;
        }

        protected override void Execute(SeleniumActionContext context)
        {
            Thread.Sleep(3000);
        }

        protected override void After(SeleniumAfterActionContext context)
        {
            Thread.Sleep(3000);
        }
    }
}
