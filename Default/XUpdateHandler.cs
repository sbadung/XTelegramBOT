using Telegram.Bot;
using Telegram.Bot.Types;
using XTelegramBOT.Update;

namespace XTelegramBOT.Default;

public delegate Task HandleMessageDelegate(Message message, ITelegramBotClient bot);

public class XUpdateHandler : IUpdateHandler
{
  public HandleMessageDelegate handleMessage;

  public XUpdateHandler(HandleMessageDelegate handleMessage)
  {
    this.handleMessage = handleMessage;
  }
  public async Task HandleUpdateAsync(ITelegramBotClient botClient, Telegram.Bot.Types.Update update, CancellationToken cancellationToken)
  {
    if (update == null || update.Message == null) return;
    var message = update.Message;

    await handleMessage(message, botClient);
  }
}