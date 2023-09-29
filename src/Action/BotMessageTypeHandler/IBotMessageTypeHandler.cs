using Telegram.Bot;
using Telegram.Bot.Types;

namespace XTelegramBOT.Action.BotMessageTypeHandler
{
  public interface IBotMessageTypeHandler
  {
    public Task Handle(ITelegramBotClient botClient, Message message, Dictionary<string, IBotCommandFunctionality>? commandFunctionalities = null);
  }
}