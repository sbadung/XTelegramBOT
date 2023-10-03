namespace XTelegramBOT.Polling;
public interface IPollingErrorHandler
{
    Task HandlePollingErrorAsync(Telegram.Bot.ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken);
}
