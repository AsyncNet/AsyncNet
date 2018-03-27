using AsyncNet.TestJobs;
using System.Threading;

namespace Rooomy.CMS.UITests.TestCases
{
    public class TestCaseFive : TestCase
    {
        private readonly TestCaseFour testCaseFour;

        public TestCaseFive(TestCaseFour testCaseFour)
        {
            this.testCaseFour = testCaseFour;
        }

        protected override void Execute(IActionContext context)
        {
            Thread.Sleep(1000);
        }

        protected override void After(IAfterActionContext context)
        {
            Thread.Sleep(1000);
        }
    }
}
