namespace AsyncNet.Jobs
{
    public class JobTaskResult
    {
        public bool ActionCanceled { get; set; } = false;

        public static JobTaskResult Failed()
        {
            return new JobTaskResult
            {
                ActionCanceled = true
            };
        }
    }
}
