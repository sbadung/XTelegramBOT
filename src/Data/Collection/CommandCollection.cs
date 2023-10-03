namespace XTelegramBOT.Data.Collection;

public static class CommandCollection
{
  public static List<string> ADMIN_COMMANDS = JSONLoader.LoadCommandsFromFile(Path.Combine(Configuration.DATA_DIRECTORY, "command/admin.json"));
  public static List<string> OWNER_COMMANDS = JSONLoader.LoadCommandsFromFile(Path.Combine(Configuration.DATA_DIRECTORY, "command/owner.json"));
  public static List<string> USER_COMMANDS = JSONLoader.LoadCommandsFromFile(Path.Combine(Configuration.DATA_DIRECTORY, "command/user.json"));

}