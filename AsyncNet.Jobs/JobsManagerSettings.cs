namespace AsyncNet.Jobs
{
    public class JobsManagerSettings
    {
        public int MaxActionsInParallel { get; set; } = 0;
        public int MaxBackwardActionsInParallel { get; set; } = 0;
    }
}