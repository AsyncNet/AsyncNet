using AsyncNet.Selenium.Common;
using System.Threading;

namespace AsyncNet.Selenium.Tests
{
    public class TestCaseOne : SeleniumTest
    {
        public string Name { get; protected set; }

        protected override void Execute(SeleniumActionContext context)
        {
            Thread.Sleep(1000);
            Name = "First test";
        }

        protected override void After(SeleniumAfterActionContext context)
        {
            Thread.Sleep(2000);
        }
    }
}
