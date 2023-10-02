using System.Text.RegularExpressions;

namespace XTelegramBOT.Utility
{
  public class ForbiddenWordController
  {
    public static bool ContainsOffensiveWord(string line)
    {
      line = LeetspeakConverter.ConvertFromLeetspeak(line);
      var pattern = GeneratePattern(Configuration.Default.FORBIDDEN_WORDS);
      return Regex.IsMatch(line, pattern, RegexOptions.IgnoreCase);
    }

    private static string GeneratePattern(List<string> words)
    {
      string[] patterns = words.Select(
          word => string.Join("", word.Select(c => $"[{c}{char.ToUpper(c)}]"))
      ).ToArray();
      return string.Join("|", patterns);
    }
  }
  public static class LeetspeakConverter
  {
    private static readonly Dictionary<string, string> leetToNormalMap = new()
        {
            {"[aA4@]", "a"}, {"[bB8]", "b"}, {"[cC(]", "c"}, {"[eE3]", "e"},
            {"[gG9]", "g"}, {"[hH#]", "h"}, {"[iI1!]", "i"}, {"[lL|]", "l"},
            {"[oO0]", "o"}, {"[sS$5]", "s"}, {"[tT7+]", "t"}, {"[zZ2]", "z"}
            /* Aggiungere altri leet options */
        };

    public static string ConvertFromLeetspeak(string leetspeak)
    {
      string converted = leetspeak;
      leetToNormalMap.AsEnumerable().ToList().ForEach(entry => converted = Regex.Replace(converted, entry.Key, entry.Value, RegexOptions.IgnoreCase));
      return converted;
    }
  }
}