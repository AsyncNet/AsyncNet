using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncNet.TestJobs
{
    public interface ITestCase
    {
        void Execute(IActionContext context);

        void Before(IBeforeActionContext context);

        void After(IAfterActionContext context);
    }
}
