using System.Text;
using System.Diagnostics;

namespace Route256Contest;

struct KVStruct
{
    public string Item1;
    public string Item2;
}
internal class Program
{
    static void Main(string[] args)
    {
        var sb = new StringBuilder();
        Stopwatch stopwatch = new();
        stopwatch.Start();

        string[] firstLine = Console.ReadLine()!.Split(" ");
        int usersCount = int.Parse(firstLine[0]);
        int requestsCount = int.Parse(firstLine[1]);

        Queue<Tuple<string, string>> requests = new();
        for (int i = 0; i < requestsCount; i++)
        {
            string[] raw = Console.ReadLine()!.Split(" ");
            //requests.Enqueue(new() { Item1 = raw[0], Item2 = raw[1] });
            requests.Enqueue(new(raw[0], raw[1]));
        }

        //System.Collections.Hashtable cachedUserRequests = new();

        System.Collections.Hashtable userRequests = new();
        for (int i = 1; i < usersCount + 1; i++)
        {
            userRequests.Add(i.ToString(), "0");
        }
        var keys = userRequests.Keys.Cast<string>().ToList();
        int j = 1;
        while(requests.TryDequeue(out var kvp))
        {
            if (kvp.Item1.Equals("2"))
            {
                //if (cachedUserRequests.ContainsKey(kvp.Item2))
                //{
                //    sb.AppendLine(cachedUserRequests[kvp.Item2].ToString());
                //    continue;
                //}
                //else
                //{
                //    cachedUserRequests.Add(kvp.Item2, userRequests[kvp.Item2]);
                //}
                sb.AppendLine(userRequests[kvp.Item2].ToString());
                continue;
            }
            if (kvp.Item2.Equals("0"))
            {
                //to all users
                foreach (var userKey in keys)
                {
                    userRequests[userKey] = j;
                }
                j++;
                continue;
            }
            //to single user
            userRequests[kvp.Item2.ToString()] = j;
            j++;
        }

        stopwatch.Stop();
        Console.WriteLine(sb.ToString());
        //Console.WriteLine(">>DBG: Elapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);
    }
}