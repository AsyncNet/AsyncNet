using System;
using System.Threading;
using AsyncNet.TestJobs;

namespace Rooomy.CMS.UITests.TestCases
{
    public class TestCaseTwo : TestCase
    {
        private readonly TestCaseOne testCaseOne;

        public string ProductNumber { get; private set; }

        public TestCaseTwo(TestCaseOne testCaseOne)
        {
            this.testCaseOne = testCaseOne;
        }

        protected override void Execute(IActionContext context)
        {
            throw new ApplicationException("EX");
            Thread.Sleep(2000);
        }

        protected override void Before(IBeforeActionContext context)
        {
            ProductNumber = "AA01";
        }

        protected override void After(IAfterActionContext context)
        {
            ////throw new ApplicationException("EX");
            Thread.Sleep(2000);
        }
    }

}
