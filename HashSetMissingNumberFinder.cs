using System.Collections.Generic;
using System.Linq;

namespace pps.solid.fmn
{
    // Strategy 2: HashSet-based approach (O(n) performance)
    public class HashSetMissingNumberFinder : IMissingNumberFinder
    {
        public List<int> FindMissingNumbers(int[] numbers)
        {
            // Handle null or empty input: return an empty list as no numbers are present
            if (numbers == null || numbers.Length == 0)
                return new List<int>();

            // Convert input array to a HashSet to remove duplicates 
            // and allow O(1) lookups when checking for missing numbers
            var numberSet = new HashSet<int>(numbers);
            // Order the unique numbers to find the range (min → max)
            var uniqueNumbers = numberSet.OrderBy(x => x).ToArray();

            if (uniqueNumbers.Length < 2)
                return new List<int>();
            // Identify the minimum and maximum values from the sequence
            int min = uniqueNumbers.First();
            int max = uniqueNumbers.Last();

            // Create a list to hold the missing numbers
            var missing = new List<int>();
            for (int i = min; i <= max; i++)
            {
                // If a number in the range is not in the original set, add it to missing list
                if (!numberSet.Contains(i))
                    missing.Add(i);
            }

            return missing;
        }
    }
}