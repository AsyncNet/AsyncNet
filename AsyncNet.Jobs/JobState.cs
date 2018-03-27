namespace AsyncNet.Jobs
{
    public enum JobState
    {
        NotSet,
        ActionWaitForParents,
        ActionWaitForSemaphore,
        ActionExecuting,
        ActionExecuted,
        ActionCanceled,
        ActionFailed,
        ActionSkipped,
        BackActionWaitForChildren,
        BackActionWaitForSemaphore,
        BackActionExecuting,
        BackActionExecuted,
        BackActionCanceled,
        BackActionFailed
    }
}
