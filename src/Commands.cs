using Telegram.Bot;

namespace XTelegramBOT.Comman;

public static class Commands
{
  public static async Task ListNamesCommandAsync(ITelegramBotClient botClient, long chatId)
  {
    /* TODO: Leggere i nomi da un file JSON */
    var names = new List<string> { "John", "Alice", "Bob", "Eve" };
    string response = "List of names:\n" + string.Join("\n", names);

    await botClient.SendTextMessageAsync(chatId, response);
  }

  public static async Task ListGroupsCommandAsync(ITelegramBotClient botClient, long chatId)
  {
    /* TODO: Leggere i gruppi da un file JSON */
    var groups = new List<string> { "Ingegneria", "Design", "Architettura" };
    string response = "List of groups:\n" + string.Join("\n", groups);

    await botClient.SendTextMessageAsync(chatId, response);
  }

  public static async Task CommandNotImplemented(ITelegramBotClient botClient, long chatId)
  {
    var response = "Work in progress, this command will be implemented soon";
    await botClient.SendTextMessageAsync(chatId, response);
  }

  public static async Task CommandNotFound(ITelegramBotClient botClient, long chatId)
  {
    var response = "Unrecognized command. Say what?";
    await botClient.SendTextMessageAsync(chatId, response);
  }
}