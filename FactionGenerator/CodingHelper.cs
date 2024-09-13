using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Metsys.Bson;

namespace FactionGenerator
{
    public class CodingHelper
    {
        public int intint { get; private set; }
        public CodingHelper()
        {

        }

    public List<string> method1()
    {
        List<string> hlper = new List<string>{"hehe","haha"};
        hlper.RemoveAll(h => !h.Equals("lmao"));
        List<string> hlperhlper = hlper.Where(h => h.Equals("hehe")).ToList();
        return hlper;
    }
    }
    public class SyntaxReference
    {
        // Variable Initialization
        public void VariableInitialization()
        {
            // Primitive Types
            int intVar = 10;
            double doubleVar = 20.5;
            char charVar = 'A';
            string stringVar = "Hello, World!";
            bool boolVar = true;

            // Nullable Types
            int? nullableIntVar = null;

            // Arrays
            int[] intArray = { 1, 2, 3, 4, 5 };

            // List
            List<string> stringList = new List<string> { "Apple", "Banana", "Cherry" };

            // Dictionary
            Dictionary<int, string> dictionary = new Dictionary<int, string>
            {
                { 1, "One" },
                { 2, "Two" },
                { 3, "Three" }
            };
        }

        // LINQ Queries
        public void LinqExamples()
        {
            // Example Data
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // LINQ Query: Filtering
            var evenNumbers = numbers.Where(n => n % 2 == 0);

            Console.WriteLine("Even Numbers:");
            foreach (var num in evenNumbers)
            {
                Console.WriteLine(num);
            }

            // LINQ Query: Sorting
            var sortedNumbers = numbers.OrderByDescending(n => n);

            Console.WriteLine("\nSorted Numbers:");
            foreach (var num in sortedNumbers)
            {
                Console.WriteLine(num);
            }

            // LINQ Query: Projection
            var squares = numbers.Select(n => n * n);

            Console.WriteLine("\nSquares of Numbers:");
            foreach (var square in squares)
            {
                Console.WriteLine(square);
            }
        }

        // Struct Example
        public struct Point
        {
            public int X { get; set; }
            public int Y { get; set; }

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public void Display()
            {
                Console.WriteLine($"Point: ({X}, {Y})");
            }
        }

        // Enum Example
        public enum DaysOfWeek
        {
            Sunday,
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday
        }

        public void EnumExample()
        {
            DaysOfWeek today = DaysOfWeek.Monday;

            Console.WriteLine($"Today is: {today}");
        }

        // Delegate Example
        public delegate int MathOperation(int a, int b);

        public void DelegateExample()
        {
            MathOperation add = (a, b) => a + b;
            MathOperation multiply = (a, b) => a * b;

            int sum = add(5, 10);
            int product = multiply(5, 10);

            Console.WriteLine($"Sum: {sum}");
            Console.WriteLine($"Product: {product}");
        }

        // Main method to run the examples
        public static void Main(string[] args)
        {
            SyntaxReference reference = new SyntaxReference();

            reference.VariableInitialization();
            reference.LinqExamples();
            reference.EnumExample();
            reference.DelegateExample();

            Point point = new Point(5, 10);
            point.Display();
        }
    }
}