using System;

namespace Lette.ProjectEuler.Core
{
    public class Solution
    {
        public int Number { get; set; }
        public string Description { get; set; }
        public long? ExpectedAnswer { get; set; }
        public long? ProposedAnswer { get; set; }
        public TimeSpan ElapsedTime { get; set; }
        public Exception Exception { get; set; }
        public bool IsCanceled { get; set; }

        private SolutionResult GetStatus()
        {
            return SolutionResult.Create(this);
        }

        public bool IsFaulted => GetStatus() == SolutionResult.Faulted;

        public bool IsCorrect => GetStatus() == SolutionResult.Pass;

        public bool IsWrong => GetStatus() == SolutionResult.Fail;

        public bool IsInconclusive => GetStatus() == SolutionResult.Inconclusive;
    }
}