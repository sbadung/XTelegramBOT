using Telegram.Bot;
using Telegram.Bot.Types;

namespace XTelegramBOT
{
    public delegate Task BotMessageHandlerActionAsync(
        ITelegramBotClient botClient,
        Message message,
        Dictionary<string, BotCommandActionAsync>? commandActionss = null
    );
    public delegate Task BotCommandActionAsync(ITelegramBotClient botClient, long chatId);
}