using System;
using System.Collections.Generic;
using System.Linq;

namespace Lette.ProjectEuler.Core
{
    public class SolutionResult
    {
        public static readonly SolutionResult Pass = new SolutionResult(0, ".", "");
        public static readonly SolutionResult Fail = new SolutionResult(1, "!", "FAIL");
        public static readonly SolutionResult Inconclusive = new SolutionResult(2, "?", "?");
        public static readonly SolutionResult Canceled = new SolutionResult(3, "c", "SKIP");
        public static readonly SolutionResult Faulted = new SolutionResult(4, "X", "ERR");

        public int Key { get; private set; }
        public string ShortDescription { get; private set; }
        public string LongDescription { get; private set; }

        private SolutionResult(int key, string shortDescription, string longDescription)
        {
            Key = key;
            ShortDescription = shortDescription;
            LongDescription = longDescription;
        }

        public static SolutionResult Create(Solution solution)
        {
            var rules = new Dictionary<Func<Solution, bool>, SolutionResult>
                {
                    { s => s.IsCanceled, Canceled },
                    { s => s.Exception != null, Faulted },
                    { s => !s.ExpectedAnswer.HasValue, Inconclusive },
                    { s => s.ExpectedAnswer != s.ProposedAnswer, Fail },
                    { s => true, Pass }
                };

            return rules.First(kvp => kvp.Key(solution)).Value;
        }
    }
}