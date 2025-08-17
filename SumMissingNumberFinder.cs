using System.Collections.Generic;
using System.Linq;

namespace pps.solid.fmn
{
    // Strategy 4: Sum-based approach (mathematical formula)
    public class SumMissingNumberFinder : IMissingNumberFinder
    {
        public List<int> FindMissingNumbers(int[] numbers)
        {
            if (numbers == null || numbers.Length == 0)
                return new List<int>();
            // Remove duplicates and sort the numbers in ascending order
            var uniqueNumbers = numbers.Distinct().OrderBy(x => x).ToArray();

            if (uniqueNumbers.Length < 2)
                return new List<int>();

            int min = uniqueNumbers.First();
            int max = uniqueNumbers.Last();

            // Calculate the expected sum of all numbers in the complete range [min..max]
            // Formula for sum of arithmetic series: (count * (first + last)) / 2
            int expectedSum = (max - min + 1) * (min + max) / 2;
            int actualSum = uniqueNumbers.Sum(); // Calculate the actual sum of the numbers present in the array

            // If only one number missing, we can find it directly
            // difference between expected and actual sums = missing number
            if (uniqueNumbers.Length == max - min)
            {
                return new List<int> { expectedSum - actualSum };
            }

            // For multiple missing, fall back to iteration
            var missing = new List<int>();
            var numberSet = new HashSet<int>(uniqueNumbers);
            for (int i = min; i <= max; i++)
            {
                if (!numberSet.Contains(i))
                    missing.Add(i);
            }

            return missing; // Return all missing numbers in the range
        }
    }
}