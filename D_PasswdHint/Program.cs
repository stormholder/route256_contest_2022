using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;

RegexOptions rxOptions = RegexOptions.Multiline | RegexOptions.Compiled;
Regex firstPassRx = new Regex(@"(?=.*[a-zA-Z])(?=.*[0-9]).+", rxOptions);
Regex secondPassRx = new Regex(@"(?=.*[aeiouy])(?=.*[bcdfghjklmnpqrstvwxz])(?=.*[0-9]).+", RegexOptions.IgnoreCase | rxOptions);
var sb = new StringBuilder();
Stopwatch stopwatch = new();
stopwatch.Start();

int userCount = int.Parse(Console.ReadLine()!);
List<string> passwords = new();
List<string> passwordsCorrected = new();

for (int i = 0; i < userCount; i++)
{
    passwords.Add(Console.ReadLine()!);
}

stopwatch.Stop();
Console.WriteLine(sb.ToString());
//Console.WriteLine(">>DBG: Elapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);
