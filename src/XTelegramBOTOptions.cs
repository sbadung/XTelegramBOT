using Telegram.Bot;

namespace XTelegramBOT;
public class XTelegramBOTOptions : TelegramBotClientOptions
{
  protected XTelegramBOTOptions(string token, string? baseUrl = null, bool useTestEnvironment = false) : base(token, baseUrl, useTestEnvironment) { }
}
