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
            int devCount = int.Parse(Console.ReadLine()!);
            List<int> devLevel = Console.ReadLine()!.Split(" ").Select(s => int.Parse(s)).ToList();


            int firstDevIdx = 0;
            int lastDevIdx = devLevel.Count;
            HashSet<int> pickedDevIndices = new();

            for (int j = 0; j < devCount / 2; j++)
            {
                if (devCount == 2)
                {
                    sb.AppendLine($"1 2");
                    continue;
                }

                for (int k = 0; k < devLevel.Count; k++)
                {
                    if (pickedDevIndices.Contains(k))
                    {
                        continue;
                    }
                    firstDevIdx = k;
                    break;
                }
                var firstDev = devLevel.ElementAt(firstDevIdx);

                pickedDevIndices.Add(firstDevIdx);

                var levelDiffs = devLevel
                    .Select((dev, index) => (index == firstDevIdx) ? 100 :Math.Abs(dev - firstDev));

                int minNr = 100;
                int minIdx = 0;
                for (int k = 0; k < levelDiffs.Count(); k++)
                {
                    if (pickedDevIndices.Contains(k))
                    {
                        continue;
                    }
                    var lDiff = levelDiffs.ElementAt(k);
                    if (lDiff < minNr)
                    {
                        minNr = lDiff;
                        minIdx = k;
                    }
                }
                pickedDevIndices.Add(minIdx);

                sb.AppendLine($"{firstDevIdx + 1} {minIdx + 1}");
            }
            sb.AppendLine();
        }

        stopwatch.Stop();
        Console.WriteLine(sb.ToString());
        //Console.WriteLine(">>DBG: Elapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);
    }
}