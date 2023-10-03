using System.Text.RegularExpressions;
using XTelegramBOT.Data.Collection;

namespace XTelegramBOT.Utility;

public static class ForbiddenController
{
  public static bool HasForbiddenDomain(string text)
  {
    return ForbiddenCollection.DOMAINS.Any(domain => text.Equals(domain, StringComparison.OrdinalIgnoreCase));
  }

  public static bool HasForbiddenWord(string content)
  {
    var regexPattern = RegexPattern(ForbiddenCollection.WORDS);
    return Regex.IsMatch(LeetspeakConverter.FromLeetSpeak(content), regexPattern, RegexOptions.IgnoreCase);
  }

  public static bool HasForbiddenContent(string content)
  {
    return HasForbiddenWord(content) || HasForbiddenDomain(content);
  }

  private static string RegexPattern(List<string> items)
  {
    string[] patterns = items.Select(item => string.Join("", item.Select(c => $"[{c}{char.ToUpper(c)}]"))).ToArray();
    return string.Join("|", patterns);
  }

  private static class LeetspeakConverter
  {
    private static readonly Dictionary<string, string> mappings = new()
    {
      {"[aA4@]", "a"}, {"[bB8]", "b"}, {"[cC(]", "c"}, {"[eE3]", "e"},
      {"[gG9]", "g"}, {"[hH#]", "h"}, {"[iI1!]", "i"}, {"[lL|]", "l"},
      {"[oO0]", "o"}, {"[sS$5]", "s"}, {"[tT7+]", "t"}, {"[zZ2]", "z"}
    };

    public static string FromLeetSpeak(string leetSpeakContent)
    {
      string content = leetSpeakContent;
      mappings.AsEnumerable()
              .ToList()
              .ForEach(entry => content = Regex.Replace(content, entry.Key, entry.Value, RegexOptions.IgnoreCase));

      return content;
    }
  }
}