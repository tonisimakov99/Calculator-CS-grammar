using System;
using System.IO;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var txt = Console.ReadLine();
            using (var txtReader = new StringReader(txt))
            {
                var parser = new ArithmeticEvalParser();
                var scanner = new Scanner();
                var result = parser.Parse(scanner.Scan(txtReader));
                Console.WriteLine(result);
            }
            Console.ReadLine();
        }
    }
}
