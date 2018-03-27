namespace AsyncNet.TestJobs
{
    public abstract class TestCase : ITestCase
    {
        void ITestCase.Before(IBeforeActionContext context)
        {
            Before(context);
        }

        void ITestCase.After(IAfterActionContext context)
        {
            After(context);
        }

        void ITestCase.Execute(IActionContext context)
        {
            Execute(context);
        }

        protected virtual void Before(IBeforeActionContext context)
        {
        }

        protected virtual void After(IAfterActionContext context)
        {
        }

        protected virtual void Execute(IActionContext context)
        {
        }
    }
}
