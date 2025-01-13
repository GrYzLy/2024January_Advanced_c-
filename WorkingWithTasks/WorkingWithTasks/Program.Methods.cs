using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    partial class Program
    {
        private static void MethodA()
        {
            TaskTitle("Stattring Method A...");
            OutputThreadInfo();
            Thread.Sleep(3000);
            TaskTitle("Finished Method A.");
        }

        private static void MethodB()
        {
            TaskTitle("Stattring Method B...");
            OutputThreadInfo();
            Thread.Sleep(2000);
            TaskTitle("Finished Method B.");
        }

        private static void MethodC()
        {
            TaskTitle("Stattring Method C...");
            OutputThreadInfo();
            Thread.Sleep(1000);
            TaskTitle("Finished Method C.");
        }

    private static decimal CallWebService()
    {
        TaskTitle("Starting call to web service...");
        OutputThreadInfo();
        Thread.Sleep(Random.Shared.Next(2000, 4000));
        TaskTitle("Finished call to web service.");
        return 89.99M;
    }
    private static string CallStoredProcedure(decimal amount)
    {
        TaskTitle("Starting call to stored procedure...");
        OutputThreadInfo();
        Thread.Sleep(Random.Shared.Next(2000, 4000));
        TaskTitle("Finished call to stored procedure.");
        return $"12 products cost more than {amount:C}.";
    }

    private static void MethodAShared()
    {
        lock (SharedObjects.Conch)
        {
            for (int i = 0; i < 5; i++)
            {
                // Simulate two seconds of work on the current thread.
                Thread.Sleep(Random.Shared.Next(2000));
                // Concatenate the letter "A" to the shared message.
                SharedObjects.Message += "A";
                // Show some activity in the console output.
                Console.Write(".");
            }
        }
    }
    private static void MethodBShared()
    {
        lock (SharedObjects.Conch)
        {
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(Random.Shared.Next(2000));
                SharedObjects.Message += "B";
                Console.Write(".");
            }
        }
    }



}

