using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;

Regex firstPassRx = new Regex(@"(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]).+", RegexOptions.Multiline | RegexOptions.Compiled);
Regex secondPassRx = new Regex(@"(?=.*[aeiouy])(?=.*[bcdfghjklmnpqrstvwxz])(?=.*[0-9]).+", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
var sb = new StringBuilder();
Stopwatch stopwatch = new();
stopwatch.Start();

int userCount = int.Parse(Console.ReadLine()!);
List<string> passwords = new();
List<string> passwordsHasUpperLowerDigit = new();
List<string> passwordsHasVowelsConsonants = new();
List<string> passwordsAddUpperLowerDigit = new();
List<string> passwordsAddVowelsConsonants = new();
List<string> resultPasswords = new();

for (int i = 0; i < userCount; i++)
{
    passwords.Add(Console.ReadLine()!);
}
foreach (string password in passwords)
{
    if (firstPassRx.IsMatch(password))
    {
        passwordsHasUpperLowerDigit.Add(password);
    }
    if (secondPassRx.IsMatch(password))
    {
        passwordsHasVowelsConsonants.Add(password);
    }
}
foreach (string password in passwords)
{
    if (resultPasswords.Contains(password))
    {
        continue;
    }
    if (passwordsHasUpperLowerDigit.Contains(password) && passwordsHasVowelsConsonants.Contains(password))
    {
        //TODO
        resultPasswords.Add(password);
    }
    if (!passwordsHasVowelsConsonants.Contains(password))
    {
        passwordsAddVowelsConsonants.Add(password);
    }
    if (!passwordsHasUpperLowerDigit.Contains(password))
    {
        passwordsAddUpperLowerDigit.Add(password);
    }
}

stopwatch.Stop();
Console.WriteLine(sb.ToString());
//Console.WriteLine(">>DBG: Elapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);
