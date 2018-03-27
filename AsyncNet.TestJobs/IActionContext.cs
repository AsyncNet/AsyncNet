namespace AsyncNet.TestJobs
{
    public interface IActionContext
    {
        string SessionId { get; }

        string TestId { get; }
    }
}
