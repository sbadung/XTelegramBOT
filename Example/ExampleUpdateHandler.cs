using Telegram.Bot;
using XTelegramBOT.Comman;
using XTelegramBOT.Update;

namespace XTelegramBOT.Example
{
  public class ExampleUpdateHandler : IUpdateHandler
  {
    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Telegram.Bot.Types.Update update, CancellationToken cancellationToken)
    {
      /* Only considering text messages */
      if (update.Message is not { } message) return;
      if (message.Text is not { } messageText) return;

      long chatId = message.Chat.Id;
      string? username = message.Chat.Username;

      /* Any idea on how to decauple this part and inject it into the class? Maybe turning actions in to tasks?*/
      var COMMANDS_ACTION = new Dictionary<string, Func<Task>>() {
        { "names", async () => await Commands.ListNamesCommandAsync(botClient, chatId) },
        { "groups", async () => await Commands.ListGroupsCommandAsync(botClient, chatId) }
      };

      if (!messageText.StartsWith("/") || messageText.Length < 2) return;
      var command = messageText[1..]; /* Removes the '/' */
      try
      {
        await COMMANDS_ACTION[command]();
      }
      catch (KeyNotFoundException ex)
      {
        bool hasCommand = Configuration.COMMANDS.Any(COMMAND => COMMAND.Command.Equals(command));
        if (hasCommand) await Commands.CommandNotImplemented(botClient, chatId);
        else await Commands.CommandNotFound(botClient, chatId);
        Console.WriteLine(ex.Message);
      }
    }
  }
}