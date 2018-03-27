using System.Threading;

namespace AsyncNet.Jobs
{
    public class JobsContext
    {
        public SemaphoreSlim ActionSemaphore { get; set; }

        public bool UseActionSemaphore { get; set; } = false;

        public SemaphoreSlim BackActionSemaphore { get; set; }

        public bool UseBackActionSemaphore { get; set; } = false;

        public object CustomData { get; set; }
    }
}
