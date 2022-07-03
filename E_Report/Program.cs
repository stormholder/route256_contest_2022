using System.Text;
using System.Diagnostics;

namespace Route256Contest;

internal class Program
{
    static void Main(string[] args)
    {
        var sb = new StringBuilder();
        Stopwatch stopwatch = new();
        stopwatch.Start();

        int entries = int.Parse(Console.ReadLine()!);
        for (int i = 0; i < entries; i++)
        {
            bool result = true;
            int taskCount = int.Parse(Console.ReadLine()!);
            Stack<int> reportStack = new();
            Console.ReadLine()!.Split(" ")
                .Select(r => int.Parse(r))
                .ToList()
                .ForEach(r => reportStack.Push(r));

            HashSet<int> prevTasks = new();
            int prevTask = reportStack.Peek();
            while(reportStack.TryPop(out var r))
            {
                if (prevTask != r && prevTasks.Contains(r))
                {
                    result = false;
                    break;
                }
                prevTask = r;
                prevTasks.Add(r);
            }
            sb.AppendLine(result ? "YES" : "NO");
        }

        stopwatch.Stop();
        Console.WriteLine(sb.ToString());
        //Console.WriteLine(">>DBG: Elapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);
    }
}