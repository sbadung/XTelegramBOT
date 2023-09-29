using Telegram.Bot;

namespace XTelegramBOT.BotCommand.BotCommandAction
{
  public abstract class AbstractBotCommandFunctionality : IBotCommandFunctionality
  {
    public abstract Task Run(ITelegramBotClient botClient, long chatId);
  }
}