using System;

namespace AsyncNet.Jobs.Events
{
    public class JobFailedArgs : JobArgs
    {
        public Exception Exception { get; set; }

        public bool HasException { get { return Exception != null; } }
    }
}
