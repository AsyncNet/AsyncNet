namespace AsyncNet.TestJobs
{
    public interface IBeforeActionContext
    {
        string SessionId { get; }

        string TestId { get; }
    }
}
