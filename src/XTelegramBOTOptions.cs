namespace XTelegramBOT;
public class XTelegramBOTOptions : Telegram.Bot.TelegramBotClientOptions
{
  protected XTelegramBOTOptions(string token, string? baseUrl = null, bool useTestEnvironment = false) : base(token, baseUrl, useTestEnvironment) { }
}
