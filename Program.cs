using XTelegramBOT.Action.BotMessageTypeHandler;
using XTelegramBOT.Action.BotMessageTypeHandler.Implementation;
using XTelegramBOT.BotCommandFunctionality.Implementation;
using XTelegramBOT.Example;
using XTelegramBOT.Polling;
using XTelegramBOT.Update;

namespace XTelegramBOT;

internal static class Program
{

  public static readonly Dictionary<string, IBotCommandFunctionality> COMMANDS_FUNCTIONALITIES = new() {
    { "groups", new ListGroups() }
  };
  
  public static readonly Dictionary<Telegram.Bot.Types.Enums.MessageType, IBotMessageTypeHandler> MESSAGE_TYPE_HANDLERS = new() {
    { Telegram.Bot.Types.Enums.MessageType.Text,  new TextMessageHandler() }
  };

  private static async Task Main()
  {
    string BOT_TOKEN = "";
    if (string.IsNullOrEmpty(BOT_TOKEN)) BOT_TOKEN = Configuration.Default.BOT_TOKEN;

    IUpdateHandler updateHandler = new ExampleUpdateHandler(MESSAGE_TYPE_HANDLERS, COMMANDS_FUNCTIONALITIES);
    IPollingErrorHandler pollingErrorHandler = new ExamplePollingErrorHandler();

    var bot = XTelegramBOT.Instance(BOT_TOKEN).Result;
    await bot.RunAsync(updateHandler, pollingErrorHandler);
  }
}
