using XTelegramBOT.Example;
using XTelegramBOT.Polling;
using XTelegramBOT.Update;

namespace XTelegramBOT;

internal class Program
{
  private static async Task Main()
  {
    IUpdateHandler updateHandler = new ExampleUpdateHandler();
    IPollingErrorHandler pollingErrorHandler = new ExamplePollingErrorHandler();

    XTelegramBOT BOT = XTelegramBOT.Instance(Configuration.BOT_TOKEN).Result;
    await BOT.RunAsync(updateHandler, pollingErrorHandler);
  }
}
