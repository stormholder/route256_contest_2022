using System.Text;
using System.Diagnostics;

namespace Route256Contest;
class User
{
    public int Id { get; set; } = 0;
    public HashSet<int> Friends { get; set; } = new();
    public HashSet<int> PossibleFriends { get; set; } = new();
}
internal class Program
{
    static void Main(string[] args)
    {
        var sb = new StringBuilder();
        Stopwatch stopwatch = new();
        stopwatch.Start();

        string[] line = Console.ReadLine()!.Split(" ");
        int userCount = int.Parse(line[0]);
        int pairCount = int.Parse(line[1]);
        Dictionary<int, HashSet<int>> friends = new();
        for (int i = 0; i < pairCount; i++)
        {
            string[] pairline = Console.ReadLine()!.Split(" ");
            int user = int.Parse(pairline[0]);
            int friend = int.Parse(pairline[1]);
            if (!friends.ContainsKey(user))
            {
                friends.Add(user, new());
            }
            friends[user].Add(friend);
            if (!friends.ContainsKey(friend))
            {
                friends.Add(friend, new() { user });
            }
            friends[friend].Add(user);
        }
        Dictionary<int, HashSet<int>> PossibleFriends = new();
        for (int j = 1; j <= userCount; j++)
        {
            PossibleFriends.Add(j, new());
        }
        foreach (var pair in friends)
        {
            HashSet<int> _possibleFriends = new();
            foreach (var f in pair.Value)
            {
                var _l2Friends = friends[f];
                foreach (var _l2f in _l2Friends)
                {
                    if (!pair.Value.Contains(_l2f) && _l2f != pair.Key)
                    {
                        _possibleFriends.Add(_l2f);
                    }
                }
            }
            //int _minCommonFriends = friends[_possibleFriends.First()].Count;
            //foreach (var pf in _possibleFriends)
            //{

            //}
            PossibleFriends[pair.Key] = _possibleFriends;
            Console.WriteLine($">>DBG: ({pair.Key})=>[{string.Join(",", pair.Value)}] ?[{string.Join(",", _possibleFriends)}]");

        }
        foreach (var pair in PossibleFriends.OrderBy(p => p.Key))
        {
            sb.AppendLine(pair.Value.Any() ? string.Join(" ", pair.Value) : "0");
        }

        stopwatch.Stop();
        Console.WriteLine(sb.ToString());
        Console.WriteLine(">>DBG: Elapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);
    }
}