using System;
using System.Text.RegularExpressions;
using AsyncNet.Jobs;

namespace AsyncNet.TestJobs
{
    public class TestJobsManager : JobsManager
    {
        public TestJobsManager(TestJobsProvider jobsProvider, JobsManagerSettings settings)
            :base(jobsProvider, settings)
        {
        }

        protected override JobsContext GetJobsContext()
        {
            return new TestJobsContext();
        }

        protected override void SetContextParams(JobsContext context)
        {
            base.SetContextParams(context);

            var ctx = (TestJobsContext)context;
            ctx.SessionId = Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "").Substring(11);
        }
    }
}
