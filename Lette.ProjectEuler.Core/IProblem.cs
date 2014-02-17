using System.Threading;

namespace Lette.ProjectEuler.Core
{
    public interface IProblem
    {
        void Prepare();
        long Solve();
        void SetCancellationToken(CancellationToken token);
    }
}