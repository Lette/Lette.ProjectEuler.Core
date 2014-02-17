using System.Collections.Generic;

namespace Lette.ProjectEuler.Core.Runner
{
    public interface IProblemSuiteBuilder
    {
        IReadOnlyList<IProblem> CreateFromAssembly(string assemblyPath);
    }
}