using AsyncNet.Jobs;
using AsyncNet.Start.Logging;
using AsyncNet.TestJobs;
using System;

namespace AsyncNet.Start
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = new GeneralLogger();

            var providerSettings = new TestJobsProviderSettings
            {
                SearchInAssemblyName = "AsyncNet.Selenium.Tests"
            };

            var jobsProvider = new TestJobsProvider(providerSettings);

            var managerSettings = new JobsManagerSettings();
            var jobsManager = new TestJobsManager(jobsProvider, managerSettings);

            jobsManager.OnStart += (s, e) => logger.Info("PROCESS START");
            jobsManager.OnFailed += (s, e) => logger.Info("PROCESS FAILED");
            jobsManager.OnFinished += (s, e) => logger.Info("PROCESS FINISHED");
            jobsManager.OnActionExecuting += (s, e) => logger.Info("START - " + ((TestCaseJob)s).TestCaseType.Name);
            jobsManager.OnActionExecuted += (s, e) => logger.Info("STOP  - " + ((TestCaseJob)s).TestCaseType.Name);
            jobsManager.OnActionFailed += (s, e) => logger.Info("FAILED - " + ((TestCaseJob)s).TestCaseType.Name + " Exception: " + e.Exception.ToString());
            jobsManager.OnBackActionExecuting += (s, e) => logger.Info("BACK START - " + ((TestCaseJob)s).TestCaseType.Name);
            jobsManager.OnBackActionExecuted += (s, e) => logger.Info("BACK STOP  - " + ((TestCaseJob)s).TestCaseType.Name);
            jobsManager.OnBackActionFailed += (s, e) => logger.Info("BACK FAILED - " + ((TestCaseJob)s).TestCaseType.Name + " Exception: " + e.Exception.ToString());
            jobsManager.OnStateChanged += (s, e) => logger.Info("STATE CHANGED - " + ((TestCaseJob)s).State.ToString() + " - " + ((TestCaseJob)s).TestCaseType.Name);

            jobsManager.BeginRun();

            bool ask = true;

            while (ask)
            {
                Console.WriteLine("c - Cancel, t - Terminate, q - Quit");
                var key = Console.ReadKey().KeyChar;

                switch(key)
                {
                    case 'c':
                        jobsManager.Cancel();
                        break;
                    case 't':
                        jobsManager.Terminate();
                        break;
                    case 'q':
                        ask = false;
                        break;
                }
            }
        }
    }
}
