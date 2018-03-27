using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AsyncNet.Jobs.Events;

namespace AsyncNet.Jobs
{
    public class Job
    {
        private readonly JobsContext jobsContext;
        private IEnumerable<Job> parents;
        private List<Job> children = new List<Job>();
        private List<Job> cleaningReadyChildren = new List<Job>();
        private Task<JobTaskResult> task;
        private Task<JobTaskResult> backTask;
        private object taskLocker = new object();
        private object backwardTaskLocker = new object();
        private JobState state = JobState.ActionWaitForParents;

        public event EventHandler<JobArgs> OnActionExecuting;
        public event EventHandler<JobFailedArgs> OnActionFailed;
        public event EventHandler<JobArgs> OnActionExecuted;
        public event EventHandler<JobArgs> OnBackActionExecuting;
        public event EventHandler<JobFailedArgs> OnBackActionFailed;
        public event EventHandler<JobArgs> OnBackActionExecuted;
        public event EventHandler<JobArgs> OnStateChanged;

        public JobState State
        {
            get
            {
                return state;
            }

            set
            {
                state = value;
                OnStateChanged?.Invoke(this, new JobArgs { });
            }
        }

        protected JobsContext JobContext
        {
            get { return jobsContext; }
        }

        protected Task<JobTaskResult> Task
        {
            get { return task; }
        }

        protected IEnumerable<Job> Parents
        {
            get { return parents ?? new Job[0]; }
        }

        public Job(JobsContext jobsContext, params Job[] parents)
        {
            this.jobsContext = jobsContext;
            this.parents = parents;

            foreach (var parent in parents)
            {
                parent.AddChild(this);
            }
        }

        public void AddChild(Job child)
        {
            if (!children.Contains(child))
            {
                children.Add(child);
            }
        }

        public Task<JobTaskResult> GetTask()
        {
            lock (taskLocker)
            {
                if (task == null)
                {
                    if (parents.Any())
                    {
                        task = Task<JobTaskResult>.Factory.ContinueWhenAll(parents.Select(x => x.GetTask()).ToArray(), x => RunAction(x));
                        State = JobState.ActionWaitForParents;
                    }
                    else
                    {
                        task = Task<JobTaskResult>.Run(() => this.RunAction(new Task<JobTaskResult>[0]));
                    }
                }
            }

            return task;
        }

        public Task<JobTaskResult> GetBackTask()
        {
            lock (backwardTaskLocker)
            {
                if (backTask == null)
                {
                    var waitFor = new List<Task<JobTaskResult>>();
                    waitFor.Add(GetTask());
                    waitFor.AddRange(children.Select(x => x.GetBackTask()));
                    backTask = Task<JobTaskResult>.Factory.ContinueWhenAll(waitFor.ToArray(), x => RunBackAction(x));
                    State = JobState.BackActionWaitForChildren;
                }
            }

            return backTask;
        }

        protected virtual void BackAction()
        {

        }

        protected virtual void Action(JobActionFeed actionFeed)
        {

        }

        private JobTaskResult RunBackAction(IEnumerable<Task<JobTaskResult>> childTasks)
        {
            if (childTasks.Any(x => x.IsFaulted))
            {
                OnActionFailed?.Invoke(this, new JobFailedArgs { Exception = new Exception("Skipped because one of the tasks failed") });
                ////State = JobState.BackActionSkipped <- back action cannot be skipped when chld action fails
            }

            var withSemaphore = jobsContext.UseBackActionSemaphore;

            if (withSemaphore)
            {
                State = JobState.BackActionWaitForSemaphore;
                jobsContext.BackActionSemaphore.Wait();
            }
            
            try
            {
                OnBackActionExecuting?.Invoke(this, new JobArgs());
                State = JobState.BackActionExecuting;
                BackAction();
                OnBackActionExecuted?.Invoke(this, new JobArgs());
                State = JobState.BackActionExecuted;
            }
            catch (Exception ex)
            {
                OnBackActionFailed?.Invoke(this, new JobFailedArgs
                {
                    Exception = ex
                });

                State = JobState.BackActionFailed;
            }

            if (withSemaphore)
            {
                jobsContext.BackActionSemaphore.Release();
            }

            return new JobTaskResult();
        }

        private JobTaskResult RunAction(IEnumerable<Task<JobTaskResult>> parentTasks)
        {
            if (parentTasks.Any(x => x.Result.ActionFailed))
            {
                State = JobState.ActionSkipped;
                OnActionFailed?.Invoke(this, new JobFailedArgs { Exception = new Exception("Skipped because one of parent tasks failed") });
                return JobTaskResult.Failed();
            }

            var result = new JobTaskResult();
            var withSemaphore = jobsContext.UseActionSemaphore;

            if (withSemaphore)
            {
                State = JobState.ActionWaitForSemaphore;
                jobsContext.ActionSemaphore.Wait();
            }
            
            try
            {
                OnActionExecuting?.Invoke(this, new JobArgs());
                State = JobState.ActionExecuting;
                Action(new JobActionFeed(result));
                OnActionExecuted?.Invoke(this, new JobArgs());
                state = JobState.ActionExecuted;
            }
            catch (Exception ex)
            {
                var args = new JobFailedArgs
                {
                    Exception = ex
                };

                result.ActionFailed = true;
                OnActionFailed?.Invoke(this, args);
                State = JobState.ActionFailed;
            }

            if (withSemaphore)
            {
                jobsContext.ActionSemaphore.Release();
            }

            return result;
        }
    }
}
