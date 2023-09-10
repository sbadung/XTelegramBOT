using XTelegramBOT.Action;
using XTelegramBOT.Example;
using XTelegramBOT.Polling;
using XTelegramBOT.Update;

namespace XTelegramBOT;

/* TODO: Add Logger */
internal static class Program
{
  private static async Task Main()
  {
    var commandActions = new Dictionary<string, BotCommandActionAsync>() {
      { "help", BotCommandAction.ListHelpAsync },
      { "names",BotCommandAction.ListNamesAsync },
      { "groups", BotCommandAction.ListGroupsAsync }
    };

    var updateHandlers = new Dictionary<Telegram.Bot.Types.Enums.MessageType, BotMessageHandlerActionAsync>() {
      { Telegram.Bot.Types.Enums.MessageType.Text, BotMessageHandlerAction.HandleTextMessageAsync },
      { Telegram.Bot.Types.Enums.MessageType.Unknown, BotMessageHandlerAction.HandleUnknownMessageAsync }
    };

    IUpdateHandler updateHandler = new ExampleUpdateHandler(updateHandlers, commandActions);
    IPollingErrorHandler pollingErrorHandler = new ExamplePollingErrorHandler();

    var bot = XTelegramBOT.Instance(Configuration.BOT_TOKEN).Result;
    await bot.RunAsync(updateHandler, pollingErrorHandler);
  }

}
