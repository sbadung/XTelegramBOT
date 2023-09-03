using Telegram.Bot;
using XTelegramBOT.Comman;
using XTelegramBOT.Update;

namespace XTelegramBOT.Example
{
  public class ExampleUpdateHandler : IUpdateHandler
  {

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Telegram.Bot.Types.Update update, CancellationToken cancellationToken)
    {
      if (update.Message is not { } message) return;
      if (message.Text is not { } messageText) return;

      long chatId = message.Chat.Id;
      string? username = message.Chat.Username;

      var COMMANDS_ACTION = new Dictionary<string, Action>() {
        { "names", async () => await Commands.ListNamesCommandAsync(botClient, chatId) },
        { "groups", async () => await Commands.ListGroupsCommandAsync(botClient, chatId) }
      };

      if (!messageText.StartsWith("/") || messageText.Length < 2) return;
      var command = messageText[1..]; /* Removes the '/' Fr the command */
      try
      {
        COMMANDS_ACTION[command]();
      }
      catch (KeyNotFoundException ex)
      {
        await Commands.CommandNotFound(botClient, chatId, command);
        Console.WriteLine(ex.Message);
      }
    }
  }
}