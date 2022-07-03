using System.Text;
using System.Diagnostics;

namespace Route256Contest;

internal class Program
{
    static long GetSeconds(string timeValue)
    {
        string[] hms = timeValue.Split(":");
        int hh = int.Parse(hms[0]);
        int mm = int.Parse(hms[1]);
        int ss = int.Parse(hms[2]);
        if (hh < 0 || hh > 23 || mm < 0 || mm > 59 || ss < 0 || ss > 59)
        {
            return -1L;
        }
        var output = hh * 3600 + mm * 60 + ss;
        //Console.WriteLine($">>DBG: {hh} {mm} {ss} = {output}");
        return output;
    }
    static void Main(string[] args)
    {
        var sb = new StringBuilder();
        Stopwatch stopwatch = new();
        stopwatch.Start();

        int entries = int.Parse(Console.ReadLine()!);
        for (int i = 0; i < entries; i++)
        {
            bool isValid = true;
            int spansCount = int.Parse(Console.ReadLine()!);
            HashSet<long> timeline = new();
            List<string> spansRaw = new();
            for (int j = 0; j < spansCount; j++)
            {
                spansRaw.Add(Console.ReadLine()!);
            }
            List<Tuple<long, long>> spans = new();
            foreach (var span in spansRaw)
            {
                string[] startEnd = span.Split("-");
                var start = GetSeconds(startEnd[0]);
                var end = GetSeconds(startEnd[1]);
                if (start < 0 || end < 0 || start > end)
                {
                    isValid = false;
                    break;
                }
                spans.Add(new(start, end));
            }
            if (!isValid)
            {
                sb.AppendLine("NO");
                continue;
            }
            for (int j = 0; j < spans.OrderBy(s => s.Item1).Count(); j++)
            {
                bool intersects = false;
                var span = spans[j];
                for (var t = span.Item1; t <= span.Item2; t++)
                {
                    if (timeline.Contains(t))
                    {
                        intersects = true;
                        break;
                    }
                    timeline.Add(t);
                }
                if (intersects)
                {
                    isValid = false;
                    break;
                }
            }
            sb.AppendLine(isValid ? "YES" : "NO");
        }

        stopwatch.Stop();
        Console.WriteLine(sb.ToString());
        //Console.WriteLine(">>DBG: Elapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);
    }
}