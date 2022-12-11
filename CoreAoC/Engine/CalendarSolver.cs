using CoreAoC.Entities;
using CoreAoC.Interfaces;
using CoreAoC.Utils;

namespace CoreAoC.Engine
{
    internal class CalendarSolver : ICalendarSolver
    {
        public int Year { get; }

        private readonly IDictionary<Problem, IEnumerable<string>> _problems;
        

        public CalendarSolver(IInputReader inputReader, int year)
        {
            Year = year;
            _problems = inputReader.GetInputs()[year];
        }


        public IList<Tuple<Result, Result>?> SolveAll()
        {
            IList<Tuple<Result, Result>?> result = new List<Tuple<Result, Result>?>();
            
            Task[] tasks = new Task[_problems.Count];
            foreach ((KeyValuePair<Problem, IEnumerable<string>> kvp, int idx) in _problems.Select((kvp, i) => (kvp, i)))
                tasks[idx] = Task.Factory.StartNew(() => kvp.Key?.Solve(kvp.Value))
                    .ContinueWith(task => result.Add(task.Result));

            Task.WaitAll(tasks);
            return result;
        }

        public Tuple<Result, Result>? SolveOne(int day)
        {
            Problem? problem = _problems.Keys.SingleOrDefault(p => p.GetType().Equals(AssemblySearcher.GetProblemFromYearAndDay(Year, day)));
            return problem?.Solve(_problems[problem]);
        }
    }
}
