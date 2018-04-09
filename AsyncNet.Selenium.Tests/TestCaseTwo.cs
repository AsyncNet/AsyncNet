using System;
using System.Threading;
using AsyncNet.Selenium.Common;

namespace AsyncNet.Selenium.Tests
{
    public class TestCaseTwo : SeleniumTest
    {
        private readonly TestCaseOne testCaseOne;

        public string ProductNumber { get; private set; }

        public TestCaseTwo(TestCaseOne testCaseOne)
        {
            this.testCaseOne = testCaseOne;
        }

        protected override void Execute(SeleniumActionContext context)
        {
            ////throw new ApplicationException("EX");
            Thread.Sleep(2000);
        }

        protected override void Before(SeleniumBeforeActionContext context)
        {
            ProductNumber = "AA01";
        }

        protected override void After(SeleniumAfterActionContext context)
        {
            ////throw new ApplicationException("EX");
            Thread.Sleep(2000);
        }
    }

}
