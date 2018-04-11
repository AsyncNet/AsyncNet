using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AsyncNet.Common;
using AsyncNet.Jobs.Events;

namespace AsyncNet.Jobs
{
    public class JobsManager
    {
        public enum JobsManagerState
        {
            Bussy,
            Ready
        }

        private readonly IJobsProvider jobsProvider;
        private readonly JobsManagerSettings settings;

        private IEnumerable<Job> jobs;
        private Task[] tasks;

        protected JobsManagerState State { get; set; } = JobsManagerState.Ready;

        public event EventHandler OnStart;
        public event EventHandler OnFinished;
        public event EventHandler OnFailed;
        public event EventHandler<JobArgs> OnActionExecuting;
        public event EventHandler<JobArgs> OnActionCanceled;
        public event EventHandler<JobFailedArgs> OnActionFailed;
        public event EventHandler<JobArgs> OnActionExecuted;
        public event EventHandler<JobArgs> OnBackActionExecuting;
        public event EventHandler<JobFailedArgs> OnBackActionFailed;
        public event EventHandler<JobArgs> OnBackActionExecuted;
        public event EventHandler<JobArgs> OnStateChanged;

        public JobsManager(IJobsProvider jobsProvider, JobsManagerSettings settings)
        {
            if (jobsProvider == null)
            {
                throw new ArgumentNullException(nameof(jobsProvider));
            }

            this.jobsProvider = jobsProvider;

            if (settings == null)
            {
                settings = new JobsManagerSettings();
            }

            this.settings = settings;
        }

        public void Run()
        {
            if (State == JobsManagerState.Bussy)
            {
                return;
            }

            State = JobsManagerState.Bussy;
            OnStart?.Invoke(this, new EventArgs());

            try
            {
                jobs = jobsProvider.GetJobs(GetJobsContext());
                AttachEvents(jobs);
                tasks = jobs.Select(x => x.GetTask()).Union(jobs.Select(x => x.GetBackTask())).ToArray();
                Task.WaitAll(tasks);
            }
            catch(AggregateException)
            {
                OnFailed?.Invoke(this, new EventArgs());
            }
            finally
            {
                State = JobsManagerState.Ready;
            }

            OnFinished?.Invoke(this, new EventArgs());
        }

        public void BeginRun()
        {
            if (State == JobsManagerState.Bussy)
            {
                return;
            }

            State = JobsManagerState.Bussy;
            OnStart?.Invoke(this, new EventArgs());

            try
            {
                jobs = jobsProvider.GetJobs(GetJobsContext());
                AttachEvents(jobs);
                tasks = jobs.Select(x => x.GetTask()).Union(jobs.Select(x => x.GetBackTask())).ToArray();
                Task.Factory.ContinueWhenAll(tasks, x =>
                    {
                        OnFinished?.Invoke(this, new EventArgs());
                        State = JobsManagerState.Ready;
                    });
            }
            catch (AggregateException)
            {
                OnFailed?.Invoke(this, new EventArgs());
                State = JobsManagerState.Ready;
            }
        }

        public void Terminate()
        {
            // finish actions (wait few seconds)
        }

        public void Cancel()
        {
            if (jobs != null && jobs.Any())
            {
                // test flag to cancel process
                // it is possible to cancel process when it waits till semaphore is released or all parents finishes action without critical error (exception)
                //
                // Job state:
                // ActionWaitForParents - default value
                // ActionWaitForSemaphore - only when we are using semaphores
                // ActionExecuting - before running Action
                // ActionExecuted - after Action finished 
                // ActionCanceled - all children set to ActionCanceled
                // ActionFailed - all children set to ActionSkipped (make it configurable(?))
                // ActionSkipped - at least one of the parent actions failed
                // BackActionWaitForChildren
                // BackActionWaitForSemaphore
                // BackActionExecuting
                // BackActionExecuted
                // BackActionCanceled - (?) not sure if I want it 
                // BackActionFailed - (?) not sure if I want it
                //
                // add flag Canceled to a Job class
                //
                // check Canceled flag before executing Action
                //
                // always run BackAction (?) whenever Action executes
            }
        }

        protected virtual JobsContext GetJobsContext()
        {
            var context = new JobsContext();
            SetContextParams(context);
            return context;
        }

        protected virtual void SetContextParams(JobsContext context)
        {
            if (settings.MaxActionsInParallel > 1)
            {
                context.ActionSemaphore = new SemaphoreSlim(settings.MaxActionsInParallel, settings.MaxActionsInParallel);
                context.UseActionSemaphore = true;
            }

            if (settings.MaxBackwardActionsInParallel > 1)
            {
                context.BackActionSemaphore = new SemaphoreSlim(settings.MaxBackwardActionsInParallel, settings.MaxBackwardActionsInParallel);
                context.UseBackActionSemaphore = true;
            }
        }

        protected virtual void AttachEvents(IEnumerable<Job> jobs)
        {
            jobs.Each(x => x.OnActionExecuting += OnActionExecuting);
            jobs.Each(x => x.OnActionExecuted += OnActionExecuted);
            jobs.Each(x => x.OnActionCancelled += OnActionCanceled);
            jobs.Each(x => x.OnActionFailed += OnActionFailed);
            jobs.Each(x => x.OnBackActionExecuting += OnBackActionExecuting);
            jobs.Each(x => x.OnBackActionExecuted += OnBackActionExecuted);
            jobs.Each(x => x.OnBackActionFailed += OnBackActionFailed);
            jobs.Each(x => x.OnStateChanged += OnStateChanged);
        }
    }
}
