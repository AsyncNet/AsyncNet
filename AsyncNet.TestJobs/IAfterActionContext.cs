namespace AsyncNet.TestJobs
{
    public interface IAfterActionContext
    {
        string SessionId { get; }

        string TestId { get; }
    }
}
