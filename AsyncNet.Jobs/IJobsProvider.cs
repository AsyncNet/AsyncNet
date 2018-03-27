using System.Collections.Generic;

namespace AsyncNet.Jobs
{
    public interface IJobsProvider
    {
        IEnumerable<Job> GetJobs(JobsContext jobsContext);
    }
}