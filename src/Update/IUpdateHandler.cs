namespace XTelegramBOT.Update;

public interface IUpdateHandler
{
    Task HandleUpdateAsync(Telegram.Bot.ITelegramBotClient botClient, Telegram.Bot.Types.Update update, CancellationToken cancellationToken);
}
