using System.Text.Json;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using XTelegramBOT.Comman;
using XTelegramBOT.Polling;
using XTelegramBOT.Update;

namespace XTelegramBOT;

internal class Program
{
  private static async Task Main()
  {
    IUpdateHandler updateHandler = new MyUpdateHandler();
    IPollingErrorHandler pollingErrorHandler = new MyPollingErrorHandler();

    var BOT = new XTelegramBOT(Configuration.BOT_TOKEN);
    await BOT.SetMyCommandsAsync(Configuration.COMMANDS);
    await BOT.RunAsync(updateHandler, pollingErrorHandler);
  }

  public class MyPollingErrorHandler : IPollingErrorHandler
  {
    public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
      var ErrorMessage = exception switch
      {
        ApiRequestException apiRequestException
          => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
        _ => exception.ToString()
      };

      Console.WriteLine(ErrorMessage);
      return Task.CompletedTask;
    }
  }
  public class MyUpdateHandler : IUpdateHandler
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
