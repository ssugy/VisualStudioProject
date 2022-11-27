using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysqlDB
{
    internal class UsingSample
    {
        public static void _Main()
        {
            string numbers = @"One
Two
Three
Four.";
            string numbers2 = "One\nTwo\nThree\nFour.";
            string letters = @"A
B
C
D.";

            using (StringReader left = new StringReader(numbers2), right = new StringReader(letters))
            {
                string? item;
                do
                {
                    item = left.ReadLine();
                    Console.Write(item);
                    Console.Write("    ");
                    item = right.ReadLine();
                    Console.WriteLine(item);
                } while (item != null);
            }
        }
    }
}
