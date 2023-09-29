using Telegram.Bot;
using Telegram.Bot.Types;

namespace XTelegramBOT.Action.BotMessageTypeHandler
{
  public abstract class AbstractBotMessageTypeHandler : IBotMessageTypeHandler
  {
    public abstract Task Handle(ITelegramBotClient botClient, Message message, Dictionary<string, IBotCommandFunctionality>? commandFunctionalities = null);

    protected static async Task HandleCommandAsync(ITelegramBotClient botClient, long chatId, string command, Dictionary<string, IBotCommandFunctionality> commandFunctionalities)
    {
      try
      {
        await commandFunctionalities[command].Run(botClient, chatId);
      }
      catch (KeyNotFoundException ex)
      {
        /* La chiave utilizzata per identificare il comando non si trovare nel dizionario: */
        IEnumerable<Telegram.Bot.Types.BotCommand> commands = await botClient.GetMyCommandsAsync();
        bool existsInCommandsList = commands.ToList().Any(command.Equals);

        if (existsInCommandsList)
        {
          /* Comando presente nell'elenco dei comandi: manca l'implementazione del comando */
          await CommandNotImplemented(botClient, chatId);
        }
        else
        {
          /* Comando inserito inesistente */
          await CommandNotFound(botClient, chatId);
        }

        Console.WriteLine(ex.Message);
      }
    }
    private static async Task CommandNotImplemented(ITelegramBotClient botClient, long chatId)
    {
      var responseContent = "Work in progress, this command will be implemented soon";
      await botClient.SendTextMessageAsync(chatId, responseContent);
    }

    private static async Task CommandNotFound(ITelegramBotClient botClient, long chatId)
    {
      var response = "Unrecognized command. Say what?";
      await botClient.SendTextMessageAsync(chatId, response);
    }

  }
}