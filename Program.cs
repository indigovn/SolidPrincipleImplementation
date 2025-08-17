using System;
using System.Collections.Generic;
using System.Linq;

namespace pps.solid.fmn
{
    class Program
    {
        static void Main(string[] args)
        {
            //OCP: Easy to modify selection criteria without changing other code
            //LSP: all algorithms work interchangeably
            Console.WriteLine("=== Missing Number Finder ===");
            Console.WriteLine("Available finder algorithms:");
            Console.WriteLine("1. LINQ (default)");
            Console.WriteLine("2. HashSet");
            Console.WriteLine("3. XOR");
            Console.WriteLine("4. Sum");
            Console.Write("Please enter choice to proceed (1-4): ");

            // Alow user to choose the finder method
            // D – Dependency Inversion Principle (DIP)
            string userChoice = Console.ReadLine();

            //I – Interface Segregation Principle (ISP)
            //The main program depends on the IMissingNumberFinder interface, not a specific implementation
            IMissingNumberFinder finder = CreateFinder(userChoice);

            Console.WriteLine($"\nFinder Method: {finder.GetType().Name}");
            Console.WriteLine("Ensure to enter numbers separated by commas.");

            while (true)
            {
                try
                {
                    Console.Write("Numbers (or 'quit'): ");
                    string input = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(input) || input.ToLower() == "quit") //Close the console
                        break;

                    var numbers = ParseInput(input);
                    // This makes switching algorithms easy (XOR method → Sum method) without changing the client code.
                    var missing = finder.FindMissingNumbers(numbers); // takes an array as the inputs and return a list of missing number(s).
                    DisplayResult(missing); //display result(s)
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                Console.WriteLine();
            }

            Console.WriteLine("Completed");
        }

        // Input processing manager
        static IMissingNumberFinder CreateFinder(string choice)
        {
            switch (choice)
            {
                case "2":
                    return new HashSetMissingNumberFinder();
                case "3":
                    return new XorMissingNumberFinder();
                case "4":
                    return new SumMissingNumberFinder();
                default:
                    return new LinqMissingNumberFinder();
            }
        }
        //Input Processing, Trims extra spaces, Removes empty strings
        //Accepts any integer value
        static int[] ParseInput(string input)
        {
            return input.Split(',')
                       .Select(s => s.Trim())
                       .Where(s => !string.IsNullOrEmpty(s))
                       .Select(s => int.TryParse(s, out int result) ? result : (int?)null)
                       .Where(n => n.HasValue)
                       .Select(n => n.Value)
                       .ToArray();
        }

        // Output Formatting
        // SRP Compliance - Only handles display logic
        static void DisplayResult(List<int> missingNumbers)
        {
            if (missingNumbers.Count == 0)
            {
                Console.WriteLine("No missing numbers found (complete sequence)");
            }
            else
            {
                Console.WriteLine($"Missing {missingNumbers.Count} numbers: [{string.Join(", ", missingNumbers)}]");
            }
        }
    }
}
