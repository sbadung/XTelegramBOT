
namespace XTelegramBOT.Data.Collection;

public static class ForbiddenCollection
{
  public static List<string> WORDS = JSONLoader.LoadForbiddenFromFile(Path.Combine(Configuration.DATA_DIRECTORY, "forbidden/word.json"));
  public static List<string> DOMAINS = JSONLoader.LoadForbiddenFromFile(Path.Combine(Configuration.DATA_DIRECTORY, "forbidden/domain.json"));
}