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

            var uniqueNumbers = numbers.Distinct().OrderBy(x => x).ToArray();

            if (uniqueNumbers.Length < 2)
                return new List<int>();

            int min = uniqueNumbers.First();
            int max = uniqueNumbers.Last();

            // If exactly one number is missing, use XOR optimization
            if (uniqueNumbers.Length == max - min)
            {
                int xorExpected = 0;
                int xorActual = 0;

                for (int i = min; i <= max; i++)
                    xorExpected ^= i;

                foreach (int num in uniqueNumbers)
                    xorActual ^= num;

                return new List<int> { xorExpected ^ xorActual };
            }

            // Fall back to standard approach for multiple missing
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