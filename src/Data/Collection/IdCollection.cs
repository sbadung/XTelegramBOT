namespace XTelegramBOT.Data.Collection;

public static class IdCollection
{
  public static List<long> ADMIN_IDS = JSONLoader.LoadIdsFromFile(Path.Combine(Configuration.DATA_DIRECTORY, "id/admin.json"));
  public static List<long> OWNER_IDS = JSONLoader.LoadIdsFromFile(Path.Combine(Configuration.DATA_DIRECTORY, "id/owner.json"));
}
