
namespace XTelegramBOT.Data;
public class JSONLoader
{
  public static List<string> LoadCommandsFromFile(string path)
  {
    var json = File.ReadAllText(path);
    return System.Text.Json.JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
  }

  public static List<long> LoadIdsFromFile(string path)
  {
    var json = File.ReadAllText(path);
    return System.Text.Json.JsonSerializer.Deserialize<List<long>>(json) ?? new List<long>();
  }

  public static List<string> LoadForbiddenFromFile(string path)
  {
    var json = File.ReadAllText(path);
    return System.Text.Json.JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
  }
}