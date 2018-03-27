namespace AsyncNet.Jobs
{
    public class JobActionFeed
    {
        private readonly JobTaskResult jobTaskResult;

        public JobActionFeed(JobTaskResult jobTaskResult)
        {
            this.jobTaskResult = jobTaskResult;
        }

        public void Cancel()
        {
            jobTaskResult.ActionFailed = true;
        }
    }
}
