using XTelegramBOT.Update;
using XTelegramBOT.Polling;
using XTelegramBOT.Default;

namespace XTelegramBOT;

internal static class Program
{
  public static async Task Main()
  {
    string BOT_TOKEN = "";
    IUpdateHandler updateHandler = new XUpdateHandler(XMessageHandler.XHandleMessageAsync);
    IPollingErrorHandler pollingErrorHandler = new XPollingErrorHandler();

    var bot = new XTelegramBOT(BOT_TOKEN);
    await bot.RunAsync(updateHandler, pollingErrorHandler);
  }
}