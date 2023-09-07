using XTelegramBOT.Example;
using XTelegramBOT.Polling;
using XTelegramBOT.Update;

namespace XTelegramBOT;

internal static class Program
{
  private static async Task Main()
  {
    IUpdateHandler updateHandler = new ExampleUpdateHandler();
    IPollingErrorHandler pollingErrorHandler = new ExamplePollingErrorHandler();

    var bot = XTelegramBOT.Instance(Configuration.BOT_TOKEN).Result;
    await bot.RunAsync(updateHandler, pollingErrorHandler);
  }
}
