// See https://aka.ms/new-console-template for more information
using System.Xml.Schema;

Console.WriteLine("Hello, World!");

int[] scores = [97, 92, 81, 60];

IEnumerable<int> scoreQuery =
    from score in scores
    where score > 80
    select score;


scoreQuery = scores.Where(i => i > 80);



foreach (var i in scoreQuery)
{
    Console.WriteLine(i);
}