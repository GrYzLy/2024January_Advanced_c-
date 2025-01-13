using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    partial class Program
    {
        private static void SectionTitle(string title)
        {
            ConsoleColor prevColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            Console.WriteLine($"*** {title}");
            Console.ForegroundColor = prevColor;
        }

        private static void TaskTitle(string title)
        {
            ConsoleColor prevColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{title}");
            Console.ForegroundColor = prevColor;
        }

        private static void OutputThreadInfo()
        {
            Thread t = Thread.CurrentThread;
            ConsoleColor prevColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(
                "Thred Id: {0}, Priority: {1}, Background: {2}, Name: {3}",
                t.ManagedThreadId, t.Priority, t.IsBackground, t.Name ?? "null");
            Console.ForegroundColor = prevColor;
        }
    }

