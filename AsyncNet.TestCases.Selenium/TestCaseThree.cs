using AsyncNet.TestJobs;
using System.Threading;

namespace Rooomy.CMS.UITests.TestCases
{
    public class TestCaseThree : TestCase
    {
        private readonly TestCaseOne testCaseOne;
        private readonly TestCaseTwo testCaseTwo;

        public TestCaseThree(TestCaseOne testCaseOne, TestCaseTwo testCaseTwo)
        {
            this.testCaseOne = testCaseOne;
            this.testCaseTwo = testCaseTwo;
        }

        protected override void Execute(IActionContext context)
        {
            Thread.Sleep(3000);
        }

        protected override void After(IAfterActionContext context)
        {
            Thread.Sleep(3000);
        }
    }
}
