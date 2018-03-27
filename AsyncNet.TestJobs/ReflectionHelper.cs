using System;
using System.Linq;
using System.Reflection;

namespace AsyncNet.TestJobs
{
    public static class ReflectionHelper
    {
        public static ParameterInfo[] GectConstructorParamTypes(Type type)
        {
            var cts = type.GetConstructors();

            if (cts.Any())
            {
                var ct = cts.First();
                return ct.GetParameters();
            }

            return new ParameterInfo[0];
        }
    }
}
