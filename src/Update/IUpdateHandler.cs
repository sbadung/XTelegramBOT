using Telegram.Bot;

namespace XTelegramBOT.Update;

public interface IUpdateHandler
{
    Task HandleUpdateAsync(ITelegramBotClient botClient, Telegram.Bot.Types.Update update, CancellationToken cancellationToken);
}
