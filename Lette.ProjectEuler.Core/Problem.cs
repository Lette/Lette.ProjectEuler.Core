using System.Threading;

namespace Lette.ProjectEuler.Core
{
    public abstract class Problem : IProblem
    {
        protected CancellationToken CancellationToken;

        public virtual void Prepare() {}

        public abstract long Solve();

        public virtual void SetCancellationToken(CancellationToken cancellationToken)
        {
            CancellationToken = cancellationToken;
        }
    }
}