using XTelegramBOT.Action;
using XTelegramBOT.Example;
using XTelegramBOT.Polling;
using XTelegramBOT.Update;

namespace XTelegramBOT;

internal static class Program
{
  public static Dictionary<string, BotCommandActionAsync> commandActions = new() {
    { "help", BotCommandAction.ListHelpAsync },
    { "names",BotCommandAction.ListNamesAsync },
    { "groups", BotCommandAction.ListGroupsAsync }
  };

  public static Dictionary<Telegram.Bot.Types.Enums.MessageType, BotMessageHandlerActionAsync> updateHandlers = new() {
    { Telegram.Bot.Types.Enums.MessageType.Text, BotMessageHandlerAction.HandleTextMessageAsync },
    { Telegram.Bot.Types.Enums.MessageType.Unknown, BotMessageHandlerAction.HandleUnknownMessageAsync }
  };

  private static async Task Main()
  {
    string BOT_TOKEN = "";
    if (string.IsNullOrEmpty(BOT_TOKEN)) BOT_TOKEN = Configuration.BOT_TOKEN;

    IUpdateHandler updateHandler = new ExampleUpdateHandler(updateHandlers, commandActions);
    IPollingErrorHandler pollingErrorHandler = new ExamplePollingErrorHandler();

    var bot = XTelegramBOT.Instance(BOT_TOKEN).Result;
    await bot.RunAsync(updateHandler, pollingErrorHandler);
  }
}
