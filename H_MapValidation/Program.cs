using System.Diagnostics;
using System.Text;

namespace Route256Contest;

//enum Direction
//{
//    SouthWest = 0,
//    SouthEast = 1,
//    East = 2,
//    NorthEast = 3,
//    NorthWest = 4,
//    West = 5
//}

struct Hexagon
{
    public int x;
    public int y;
    public int color;
}

class HexRegion
{
    private int _C;
    private LinkedList<Hexagon> _adj = new();
    public int Color { get { return _C; } }
    public LinkedList<Hexagon> Adj { get { return _adj; } }
    public HexRegion(int c) => _C = c;

    public void AddEdge(Hexagon hex)
    {
        if (!_adj.Any(h => h.x == hex.x && h.y == hex.y))
            _adj.AddLast(hex);
    }
}

internal class Program
{
    static readonly int[] Directions = new int[6] { 0, 1, 2, 3, 4, 5 };
    static readonly int[,,] oddr_direction_differences = new int[2, 6, 2] {
        // even rows 
        {   { 1, 0 }, { 0, -1 }, { -1, -1 }, { -1, 0 }, { -1, 1 }, { 0, 1 } },
        // odd rows
        {   { 1, 0 }, { 1, -1 }, { 0, -1 }, { -1, 0 }, { 0, 1 }, { 1, 1 } }
    };
    static Hexagon oddr_offset_neighbor(int X, int Y, int direction)
    {
        var parity = Y & 1;
        var diffX = oddr_direction_differences[parity, direction, 0];
        var diffY = oddr_direction_differences[parity, direction, 1];
        return new Hexagon() { x = X + diffX, y = Y + diffY };
    }

    static HexRegion BFS(char[,] map, bool[,] vis, int row, int col)
    {
        HexRegion result = new(map[row, col]);
        Queue<Hexagon> q = new();
        q.Enqueue(new Hexagon() { x = col, y = row, color = map[row, col] });
        vis[row, col] = true;
        int height = map.GetLength(0);
        int width = map.GetLength(1);

        while (q.Count > 0)
        {
            var hex = q.Peek();
            result.AddEdge(hex);
            q.Dequeue();
            foreach (var d in Directions)
            {
                var neigh = oddr_offset_neighbor(hex.x, hex.y, d);
                if (
                    neigh.x < 0 ||
                    neigh.y < 0 ||
                    neigh.x >= width ||
                    neigh.y >= height ||
                    map[neigh.y, neigh.x] == 0 ||
                    map[neigh.y, neigh.x] != hex.color ||
                    vis[neigh.y, neigh.x] == true
                    )
                {
                    continue;
                }
                neigh.color = map[neigh.y, neigh.x];
                q.Enqueue(neigh);
                result.AddEdge(neigh);
                vis[neigh.y, neigh.x] = true;
            }
        }
        return result;
    }

    static void Main(string[] args)
    {
        var sb = new StringBuilder();
        Stopwatch stopwatch = new();
        stopwatch.Start();

        int entries = int.Parse(Console.ReadLine()!);
        for (int i = 0; i < entries; i++)
        {
            var dimensions = Console.ReadLine()!.Split(" ");
            int height = int.Parse(dimensions[0]);
            int width = (int.Parse(dimensions[1]) + 1) / 2;
            char[,] map = new char[height, width];
            bool[,] visited = new bool[height, width];


            //Read hexagon
            for (int j = 0; j < height; j++)
            {
                char[] line = Console.ReadLine()!.ToCharArray();
                int kk = 0;
                for (int k = 0; k < line.Length; k++)
                {
                    if (kk == width)
                    {
                        break;
                    }
                    if (line[k] == '.')
                    {
                        continue;
                    }
                    map[j, kk] = line[k];
                    kk++;
                }
            }
            for (int j = 0; j < height; j++)
            {
                for (int k = 0; k < width; k++)
                {
                    if (map[j,k] == 0)
                    {
                        visited[j, k] = true;
                    }
                }
            }

            //Get neighbors
            List<HexRegion> _r = new();
            for (int j = 0; j < height; j++)
            {
                for (int k = 0; k < width; k++)
                {
                    if (visited[j,k])
                    {
                        continue;
                    }
                    _r.Add(BFS(map, visited, j, k));
                }
            }
            Dictionary<int, List<HexRegion>> regions = _r.GroupBy(hr => hr.Color).ToDictionary(g => g.Key, g => g.ToList());
            sb.AppendLine(regions.Any(r => r.Value.Count > 1) ? "NO" : "YES");
        }

        stopwatch.Stop();
        Console.WriteLine(sb.ToString());
        Console.WriteLine(">>DBG: Elapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);
    }
}