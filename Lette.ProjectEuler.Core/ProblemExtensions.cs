using System.Linq;

namespace Lette.ProjectEuler.Core
{
    public static class ProblemExtensions
    {
        public static EulerAttribute GetMetaData(this IProblem problem)
        {
            return (EulerAttribute)problem
                .GetType()
                .GetCustomAttributes(false)
                .First(a => a.GetType() == typeof(EulerAttribute));
        }
    }
}