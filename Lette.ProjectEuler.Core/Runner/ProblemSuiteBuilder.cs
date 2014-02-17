using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Lette.ProjectEuler.Core.Runner
{
    public class ProblemSuiteBuilder : IProblemSuiteBuilder
    {
        public IReadOnlyList<IProblem> CreateFromAssembly(string assemblyPath)
        {
            var problems = Assembly.LoadFrom(assemblyPath)
                .GetTypes()
                .Where(t => t.IsDefined(typeof(EulerAttribute), false))
                .Where(t => t.GetInterfaces().Contains(typeof(IProblem)))
                .Select(t => (IProblem)t.GetConstructor(Type.EmptyTypes).Invoke(null))
                .OrderBy(p => p.GetMetaData().Number)
                .ToList();

            return new List<IProblem>(problems).AsReadOnly();
        }
    }
}