
namespace AsyncNet.TestJobs
{
    public class TestCaseContext : IActionContext, IAfterActionContext, IBeforeActionContext
    {
        public string SessionId { get; set; }

        public string TestId { get; set; }
    }
}
