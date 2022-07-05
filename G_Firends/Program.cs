using System.Text;
using System.Diagnostics;
using System.Linq;

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
        foreach (var friend in friends)
        {
            HashSet<int> _possibleFriends = new();
            foreach (var _f in friend.Value.SelectMany(f => friends[f].Where(l2f => !friend.Value.Contains(l2f) && l2f != friend.Key)))
            {
                _possibleFriends.Add(_f);
            }

            PossibleFriends[friend.Key] = _possibleFriends;
        }
        foreach (var possibleFriend in PossibleFriends.OrderBy(p => p.Key))
        {
            if (!possibleFriend.Value.Any())
            {
                sb.AppendLine("0");
                continue;
            }
            var _l2friends = friends[possibleFriend.Key];
            Dictionary<int, HashSet<int>> commonFriends = new();
            foreach (var pf in possibleFriend.Value)
            {
                commonFriends.Add(pf, friends[pf].Where(f => _l2friends.Contains(f)).ToHashSet());
            }
            int maxFriends = commonFriends.Values.Select(v => v.Count).Max();
            sb.AppendLine(
                string.Join(" ", 
                            commonFriends
                                .Where(cf => cf.Value.Count == maxFriends)
                                .Select(c=>c.Key)
                                .OrderBy(c => c)
                )
            );
        }

        stopwatch.Stop();
        Console.WriteLine(sb.ToString());
        //Console.WriteLine(">>DBG: Elapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);
    }
}