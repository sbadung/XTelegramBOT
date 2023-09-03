using Telegram.Bot;

namespace XTelegramBOT.Polling;
public interface IPollingErrorHandler
{
    Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken);
}
