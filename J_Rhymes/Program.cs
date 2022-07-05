
using System.Text;
using System.Diagnostics;

var sb = new StringBuilder();
Stopwatch stopwatch = new();
int dictSize = int.Parse(Console.ReadLine()!);
HashSet<char[]> dictionary = new();
List<char[]> requests = new();
for (int i = 0; i < dictSize; i++)
{
    dictionary.Add(Console.ReadLine()!.Reverse().ToArray());
}
int requestsCount = int.Parse(Console.ReadLine()!);
for (int i = 0; i < requestsCount; i++)
{
    requests.Add(Console.ReadLine()!.Reverse().ToArray());
}

Dictionary<char[], string> cachedResults = new();

stopwatch.Start();
foreach (var request in requests)
{
    if (cachedResults.ContainsKey(request))
    {
        sb.AppendLine(cachedResults[request]);
        continue;
    }
    int reqLen = request.Length;
    int rhymeIdx = 0;
    int max = 0;
    int i = 0;
    foreach (var word in dictionary.OrderBy(w => (int)w[0]))
    {
        if (request[0] != word[0])
        {
            i++;
            continue;
        }
        int wordLen = word.Length;
        if (wordLen == reqLen && word[0] == request[0] && word[wordLen - 1] == request[wordLen - 1])
        {
            i++;
            continue;
        }
        int maxCount = (wordLen > reqLen) ? reqLen : wordLen;
        int j = 0;
        while (j < maxCount && word[j].Equals(request[j]))
        {
            j++;
        }
        if (j > max)
        {
            max = j;
            rhymeIdx = i;
        }
        i++;
    }
    var _randomRhyme = dictionary.ElementAt(rhymeIdx);
    var bestRhymeString = string.Join("", _randomRhyme.Reverse());
    cachedResults.Add(request, bestRhymeString);
    sb.AppendLine(bestRhymeString);
}
stopwatch.Stop();

Console.WriteLine(sb.ToString());
Console.WriteLine(">>DBG: Elapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);