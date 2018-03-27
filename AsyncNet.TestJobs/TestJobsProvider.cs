using AsyncNet.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AsyncNet.TestJobs
{
    public class TestJobsProvider : IJobsProvider
    {
        private readonly TestJobsProviderSettings settings;

        public TestJobsProvider(TestJobsProviderSettings settings)
        {
            this.settings = settings;
        }

        public IEnumerable<Job> GetJobs(JobsContext jobsContext)
        {
            var registeredTests = Assembly.Load(settings.SearchInAssemblyName).GetTypes().Where(x => x != typeof(ITestCase) && x != typeof(ITestCase) && typeof(ITestCase).IsAssignableFrom(x) && !x.IsAbstract);

            var jobsBuilder = new JobsBuilder<Type, TestCaseJob>(
                x => ReflectionHelper.GectConstructorParamTypes(x).Select(t => t.ParameterType),
                (t, d) => new TestCaseJob(t, (TestJobsContext)jobsContext, d.ToArray()));

            return jobsBuilder.GetJobs(registeredTests);
        }
    }
}
