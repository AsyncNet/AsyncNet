using AsyncNet.TestJobs;
using System.Threading;

namespace Rooomy.CMS.UITests.TestCases
{
    public class TestCaseFour : TestCase
    {
        private readonly TestCaseOne testCaseOne;

        public TestCaseFour(TestCaseOne testCaseOne)
        {
            this.testCaseOne = testCaseOne;
        }

        protected override void Execute(IActionContext context)
        {
            Thread.Sleep(6000);
        }

        protected override void After(IAfterActionContext context)
        {
            Thread.Sleep(4000);
        }
    }
}
