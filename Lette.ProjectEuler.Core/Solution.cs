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

        public bool IsFaulted
        {
            get { return GetStatus() == SolutionResult.Faulted; }
        }

        public bool IsCorrect
        {
            get { return GetStatus() == SolutionResult.Pass; }
        }

        public bool IsWrong
        {
            get { return GetStatus() == SolutionResult.Fail; }
        }

        public bool IsInconclusive
        {
            get { return GetStatus() == SolutionResult.Inconclusive; }
        }
    }
}