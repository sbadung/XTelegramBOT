using Telegram.Bot;

using XTelegramBOT.Polling;
using XTelegramBOT.Update;

namespace XTelegramBOT;

public class XTelegramBOT : TelegramBotClient
{
  public XTelegramBOT(XTelegramBOTOptions options, HttpClient? httpClient = null) : base(options, httpClient) { }
  public XTelegramBOT(string token, HttpClient? httpClient = null) : base(token, httpClient) { }

  public async Task RunAsync(IUpdateHandler updateHandler, IPollingErrorHandler pollingErrorHandler)
  {
    using CancellationTokenSource cts = new();

    Telegram.Bot.Polling.ReceiverOptions receiverOptions = new()
    {
      AllowedUpdates = Array.Empty<Telegram.Bot.Types.Enums.UpdateType>()
    };

    this.StartReceiving(
      updateHandler: updateHandler.HandleUpdateAsync,
      pollingErrorHandler: pollingErrorHandler.HandlePollingErrorAsync,
      receiverOptions: receiverOptions,
      cancellationToken: cts.Token
    );

    var me = await this.GetMeAsync(cancellationToken: cts.Token);
    Console.WriteLine($"Listening for @{me.Username}");
    Console.WriteLine($"Press any key to stop listening");
    Console.ReadLine();
    cts.Cancel();
  }
}