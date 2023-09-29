using Telegram.Bot;

public interface IBotCommandFunctionality
{
  public Task Run(ITelegramBotClient botClient, long chatId);
}