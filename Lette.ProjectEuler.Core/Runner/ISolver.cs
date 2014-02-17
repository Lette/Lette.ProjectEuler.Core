using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lette.ProjectEuler.Core.Runner
{
    public interface ISolver
    {
        void SolveAll(IEnumerable<IProblem> problems, Action<Solution> callback, bool runParallel);
        Task SolveAllAsync(IEnumerable<IProblem> problems, Action<Solution> callback, bool runParallel);
        void Cancel();
    }
}