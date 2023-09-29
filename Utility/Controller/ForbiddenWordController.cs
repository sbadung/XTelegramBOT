using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace XTelegramBOT.Utility
{
  public class ForbiddenWordController
  {
    public static bool ContainsOffensiveWord(string line)
    {
      return Regex.IsMatch(line, GeneratePattern(Configuration.Default.FORBIDDEN_WORDS), RegexOptions.IgnoreCase);
    }

    private static string GeneratePattern(List<string> words)
    {
      string[] patterns = words.Select(word => string.Join("", word.Select(c => $"[{c}{char.ToUpper(c)}]"))).ToArray();
      return string.Join("|", patterns);
    }
  }
}