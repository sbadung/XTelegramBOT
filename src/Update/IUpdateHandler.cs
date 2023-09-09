using Telegram.Bot;

namespace XTelegramBOT.Update;

public interface IUpdateHandler
{
    Task HandleUpdate(ITelegramBotClient botClient, Telegram.Bot.Types.Update update, CancellationToken cancellationToken);
}
