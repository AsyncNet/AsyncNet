using AsyncNet.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AsyncNet.TestJobs
{
    public class TestCaseJob : Job
    {
        private readonly Type testCaseType;

        private ITestCase instance;
        private TestCaseContext testCaseContext;
        private object locker = new object();

        public Type TestCaseType { get { return testCaseType; } }

        protected TestCaseContext TestCaseContext
        {
            get
            {
                testCaseContext = testCaseContext ?? CreateTestCaseContext();
                return testCaseContext;
            }
        }

        public TestCaseJob(Type testCaseType, TestJobsContext jobContext, TestCaseJob[] dependsOn)
            : base(jobContext, dependsOn)
        {
            this.testCaseType = testCaseType;
        }

        public ITestCase GetInstance()
        {
            lock (locker)
            {
                instance = instance ?? CreateInstance();
                return instance;
            }
        }

        protected override void BackAction()
        {
            var testCase = GetInstance();

            IAfterActionContext context = TestCaseContext;
            testCase.After(context);
        }

        protected override void Action(JobActionFeed actionFeed)
        {
            var testCase = GetInstance();

            try
            {
                IBeforeActionContext beforeContext = TestCaseContext;
                testCase.Before(beforeContext);
            }
            catch
            {
                // don't run child tasks
                actionFeed.Cancel();
            }

            try
            {
                IActionContext testContext = TestCaseContext;
                testCase.Execute(testContext);
            }
            catch
            {
                // don't run child tasks
                actionFeed.Cancel();
            }
        }

        private ITestCase CreateInstance()
        {
            var pars = ReflectionHelper.GectConstructorParamTypes(testCaseType);

            if (pars.Any())
            {
                var relatedTestJobs = new List<object>();

                foreach (var par in pars)
                {
                    var relatedJob = Parents.Cast<TestCaseJob>().Single(x => x.TestCaseType.FullName == par.ParameterType.FullName);
                    relatedTestJobs.Add(relatedJob.GetInstance());
                }

                return Activator.CreateInstance(testCaseType, relatedTestJobs.ToArray()) as ITestCase;
            }

            return Activator.CreateInstance(testCaseType) as ITestCase;
        }

        private TestCaseContext CreateTestCaseContext()
        {
            return new TestCaseContext
            {
                SessionId = ((TestJobsContext)JobContext).SessionId,
                TestId = Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "").Substring(11)
            };
        }
    }
}
