using System.Collections.Generic;
using System.Linq;

namespace pps.solid.fmn
{
    // Strategy 1: LINQ-based approach (clean and readable)
    public class LinqMissingNumberFinder : IMissingNumberFinder
    {
        public List<int> FindMissingNumbers(int[] numbers)
        {
            if (numbers == null || numbers.Length == 0)
                return new List<int>();

            var uniqueNumbers = numbers.Distinct().OrderBy(x => x).ToArray();

            if (uniqueNumbers.Length < 2)
                return new List<int>();

            int min = uniqueNumbers.First();
            int max = uniqueNumbers.Last();

            var completeRange = Enumerable.Range(min, max - min + 1);
            return completeRange.Except(uniqueNumbers).ToList();
        }
    }
}
