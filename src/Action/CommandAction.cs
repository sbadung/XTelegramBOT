using Telegram.Bot;

namespace XTelegramBOT.Action;

public static class CommandAction
{

  public delegate Task CommandActionDelegate(ITelegramBotClient bot, long id);
  public static Dictionary<string, CommandActionDelegate> ACTIONS = new()
  {
    { "groups", Group.ListAll },
  };

  public static bool HasCommand(string command)
  {
    return ACTIONS.ContainsKey(command);
  }

  public static async Task RunCommand(string command, ITelegramBotClient bot, long chatId)
  {
    await ACTIONS[command](bot, chatId);
  }

  public static async Task UnauthorizedToRunCommand(string command, ITelegramBotClient bot, long chatId)
  {
    await bot.SendTextMessageAsync(chatId, $"Unauthorized to run command: {command}");
  }
  
  public static async Task CommandNotFound(string command, ITelegramBotClient bot, long chatId)
  {
    await bot.SendTextMessageAsync(chatId, $"Unable to find searched command: {command}");
  }
  

}