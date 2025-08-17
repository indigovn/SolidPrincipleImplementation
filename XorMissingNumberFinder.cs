using System.Collections.Generic;
using System.Linq;

namespace pps.solid.fmn
{
    // Strategy 3: XOR-based approach (optimized for single missing number)
    public class XorMissingNumberFinder : IMissingNumberFinder
    {
        public List<int> FindMissingNumbers(int[] numbers)
        {
            if (numbers == null || numbers.Length == 0)
                return new List<int>();

            // Remove duplicates and sort the input in ascending order
            var uniqueNumbers = numbers.Distinct().OrderBy(x => x).ToArray();

            if (uniqueNumbers.Length < 2)
                return new List<int>();

            int min = uniqueNumbers.First();
            int max = uniqueNumbers.Last();

            // Case 1: Exactly one number is missing → use XOR trick
            // XOR all numbers in the expected full range [min..max]
            // and XOR all actual numbers from input.
            // The missing number will be the difference (xorExpected ^ xorActual).
            if (uniqueNumbers.Length == max - min)
            {
                int xorExpected = 0;
                int xorActual = 0;

                for (int i = min; i <= max; i++) // XOR all numbers in the complete range
                    xorExpected ^= i;

                foreach (int num in uniqueNumbers)  // XOR all numbers in the actual sequence
                    xorActual ^= num;

                return new List<int> { xorExpected ^ xorActual }; // XOR of xorExpected and xorActual gives the missing number
            }

            // Case 2: More than one number is missing → XOR method won’t work.
            // Fall back to iteration with HashSet lookup (O(1) average case).
            var missing = new List<int>();
            var numberSet = new HashSet<int>(uniqueNumbers);
            for (int i = min; i <= max; i++)
            {
                if (!numberSet.Contains(i))
                    missing.Add(i);
            }

            return missing;
        }
    }
}