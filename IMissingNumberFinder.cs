using System.Collections.Generic;

namespace pps.solid.fmn
{
    // Single interface for all finder implementation
    public interface IMissingNumberFinder
    {
        List<int> FindMissingNumbers(int[] numbers);
    }
}