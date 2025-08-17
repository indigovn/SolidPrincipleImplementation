using System.Collections.Generic;
using System.Linq;

namespace pps.solid.fmn
{
    // Strategy 1: LINQ-based approach (clean and readable)
    public class LinqMissingNumberFinder : IMissingNumberFinder
    {
        public List<int> FindMissingNumbers(int[] numbers)
        {
            // Handle edge case: if input is null or empty, return an empty list
            if (numbers == null || numbers.Length == 0)
                return new List<int>();

            // Remove duplicates and sort the numbers in ascending order
            // Distinct() ensures only unique values remain
            // OrderBy(x => x) sorts them
            var uniqueNumbers = numbers.Distinct().OrderBy(x => x).ToArray();

            // If there are fewer than 2 unique numbers, no missing numbers can exist
            if (uniqueNumbers.Length < 2)
                return new List<int>();

            int min = uniqueNumbers.First();
            int max = uniqueNumbers.Last();
            // Generate the complete range of numbers from min → max (inclusive)
            var completeRange = Enumerable.Range(min, max - min + 1);

            // Find numbers in the complete range that are not in the input
            // Except() efficiently subtracts the known numbers from the full range
            return completeRange.Except(uniqueNumbers).ToList();
        }
    }
}
