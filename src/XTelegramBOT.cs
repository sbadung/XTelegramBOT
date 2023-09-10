using Telegram.Bot;
using Telegram.Bot.Types;
using XTelegramBOT.Polling;
using XTelegramBOT.Update;

namespace XTelegramBOT;

public class XTelegramBOT : TelegramBotClient
{
  private XTelegramBOT(XTelegramBOTOptions options, HttpClient? httpClient = null) : base(options, httpClient) { }
  private XTelegramBOT(string token, HttpClient? httpClient = null) : base(token, httpClient) { }

  public static async Task<XTelegramBOT> Instance(XTelegramBOTOptions options, HttpClient? httpClient = null, IEnumerable<BotCommand>? commands = null)
  {
    var bot = new XTelegramBOT(options, httpClient);
    await bot.SetMyCommandsAsync(commands ?? Configuration.COMMANDS);
    return bot;
  }

  public static async Task<XTelegramBOT> Instance(string token, HttpClient? httpClient = null, IEnumerable<BotCommand>? commands = null)
  {
    var bot = new XTelegramBOT(token, httpClient);
    await bot.SetMyCommandsAsync(commands ?? Configuration.COMMANDS);
    return bot;
  }

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