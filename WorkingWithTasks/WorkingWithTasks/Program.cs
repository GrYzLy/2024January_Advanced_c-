using System.Diagnostics;

OutputThreadInfo();
Stopwatch sw = Stopwatch.StartNew();


//SectionTitle("Running methods sync on one thread.");
//MethodA();
//MethodB();
//MethodC();
//Console.WriteLine($"{sw.ElapsedMilliseconds:#,##0}ms elapsed."); //20 009ms elapsed.

//SectionTitle("Running methods ASYNC on MULTIPLE thread.");
//Task taskA = new(MethodA);
//taskA.Start();

//Task taskB = Task.Factory.StartNew(MethodB);
//Task taskC = Task.Run(MethodC);

//Console.WriteLine($"{sw.ElapsedMilliseconds:#,##0}ms elapsed.");

//SectionTitle("Running methods ASYNC on MULTIPLE thread.");

//Task[] tasks = { taskA, taskB, taskC };
//Task.WaitAll(tasks);

//Console.WriteLine($"{sw.ElapsedMilliseconds:#,##0}ms elapsed.");

//sw.Restart();


//SectionTitle("Passing the result of one task as an input into another.");
//Task<string> taskServiceThenSProc = Task.Factory
//   .StartNew(CallWebService) // returns Task<decimal>
//  .ContinueWith(previousTask => // returns Task<string>
//    CallStoredProcedure(previousTask.Result));
//Console.WriteLine($"Result: {taskServiceThenSProc.Result}");
//Console.WriteLine($"{sw.ElapsedMilliseconds:#,##0}ms elapsed.");

sw.Restart();

Console.WriteLine("Please wait for the tasks to complete.");

Task a = Task.Factory.StartNew(MethodAShared);
Task b = Task.Factory.StartNew(MethodBShared);

Task.WaitAll(new Task[] { a, b });
Console.WriteLine();
Console.WriteLine($"Results: {SharedObjects.Message}.");
Console.WriteLine($"{sw.ElapsedMilliseconds:N0} elapsed milliseconds.");





