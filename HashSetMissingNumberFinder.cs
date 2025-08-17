using System.Collections.Generic;
using System.Linq;

namespace pps.solid.fmn
{
    // Strategy 2: HashSet-based approach (O(n) performance)
    public class HashSetMissingNumberFinder : IMissingNumberFinder
    {
        public List<int> FindMissingNumbers(int[] numbers)
        {
            if (numbers == null || numbers.Length == 0)
                return new List<int>();

            var numberSet = new HashSet<int>(numbers);
            var uniqueNumbers = numberSet.OrderBy(x => x).ToArray();

            if (uniqueNumbers.Length < 2)
                return new List<int>();

            int min = uniqueNumbers.First();
            int max = uniqueNumbers.Last();

            var missing = new List<int>();
            for (int i = min; i <= max; i++)
            {
                if (!numberSet.Contains(i))
                    missing.Add(i);
            }

            return missing;
        }
    }
}