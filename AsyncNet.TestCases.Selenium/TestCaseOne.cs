using AsyncNet.TestJobs;
using System.Threading;

namespace Rooomy.CMS.UITests.TestCases
{
    public class TestCaseOne : TestCase
    {
        public string Name { get; protected set; }

        protected override void Execute(IActionContext context)
        {
            Thread.Sleep(1000);
            Name = "First test";
        }

        protected override void After(IAfterActionContext context)
        {
            Thread.Sleep(2000);
        }
    }
}
