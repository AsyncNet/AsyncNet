using AsyncNet.Jobs;

namespace AsyncNet.TestJobs
{
    public class TestJobsContext : JobsContext
    {
        public string SessionId { get; set; }
    }
}
