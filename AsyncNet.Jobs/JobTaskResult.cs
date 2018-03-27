namespace AsyncNet.Jobs
{
    public class JobTaskResult
    {
        public bool ActionFailed { get; set; } = false;

        public static JobTaskResult Failed()
        {
            return new JobTaskResult
            {
                ActionFailed = true
            };
        }
    }
}
