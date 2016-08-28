using System.Collections.Generic;
using System.Reflection;
using Lette.ProjectEuler.Core.Runner;
using Xunit.Sdk;

namespace Lette.ProjectEuler.Core.XUnit
{
    public class EulerDataAttribute : DataAttribute
    {
        private readonly string _assemblyPath;

        public EulerDataAttribute(string assemblyPath)
        {
            _assemblyPath = assemblyPath;
        }

        public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest)
        {
            var builder = new ProblemSuiteBuilder();
            var suite = builder.CreateFromAssembly(_assemblyPath);

            foreach (var problem in suite)
            {
                problem.Prepare();
                var metadata = problem.GetMetaData();
                yield return new object[] {$"{metadata.Number,4}", metadata.Description, metadata.Answer, problem.Solve() };
            }
        }
    }
}