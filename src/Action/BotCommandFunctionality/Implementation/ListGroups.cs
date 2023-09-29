using Telegram.Bot;
using XTelegramBOT.BotCommand.BotCommandAction;

namespace XTelegramBOT.BotCommandFunctionality.Implementation
{
  public class ListGroups : AbstractBotCommandFunctionality
  {
    public override async Task Run(ITelegramBotClient botClient, long chatId)
    {
      /* TODO: Aggiungere metodo per la ricerca e formattazione dei gruppi */
      await botClient.SendTextMessageAsync(chatId, string.Join(" ", new List<string> { "Ingegneria", "Design", "Architettura" }));
    }
  }
}