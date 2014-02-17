using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Lette.ProjectEuler.Core.Runner
{
    public class Solver : ISolver
    {
        private Action<Solution> _callback;
        private CancellationTokenSource _tokenSource;

        public async Task SolveAllAsync(
            IEnumerable<IProblem> problems, Action<Solution> callback, bool runParallel)
        {
            await Task.Run(() => SolveAll(problems, callback, runParallel));
        }

        public void SolveAll(IEnumerable<IProblem> problems, Action<Solution> callback, bool runParallel)
        {
            _tokenSource = new CancellationTokenSource();
            _callback = callback;

            var options = new ParallelOptions();
            options.MaxDegreeOfParallelism = runParallel ? -1 : 1;

            Parallel.ForEach(problems, options, (problem, loopState) =>
                {
                    if (_tokenSource.IsCancellationRequested)
                    {
                        loopState.Stop();
                    }

                    Solve(problem, _tokenSource.Token);
                });
        }

        private void Solve(IProblem problem, CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                return;
            }

            var stopwatch = new Stopwatch();

            var metaData = problem.GetMetaData();

            problem.SetCancellationToken(token);

            var canceled = false;
            Exception exception = null;
            long? result = null;

            try
            {
                // Use "Prepare" for loading initial data structures that are part of the problem definition,
                // and which should not - with a good conscience - be a part of the timed solution.
                problem.Prepare();

                stopwatch.Reset();
                stopwatch.Start();

                result = problem.Solve();
            }
            catch (OperationCanceledException)
            {
                canceled = true;
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            finally
            {
                stopwatch.Stop();
            }

            var solution = new Solution();

            solution.Number = metaData.Number;
            solution.Description = metaData.Description;
            solution.ExpectedAnswer = metaData.Answer;
            solution.ProposedAnswer = result;
            solution.IsCanceled = canceled;
            solution.Exception = exception;
            solution.ElapsedTime = stopwatch.Elapsed;

            _callback(solution);
        }

        public void Cancel()
        {
            if (_tokenSource == null)
            {
                return;
            }

            if (_tokenSource.IsCancellationRequested)
            {
                return;
            }

            _tokenSource.Cancel();
        }
    }
}