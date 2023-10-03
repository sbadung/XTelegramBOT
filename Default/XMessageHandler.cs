using Telegram.Bot;
using Telegram.Bot.Types;
using XTelegramBOT.Action;
using XTelegramBOT.Permission;
using XTelegramBOT.Utility;

namespace XTelegramBOT.Default;

public static class XMessageHandler
{
  public static HandleMessageDelegate XHandleMessageAsync = async (Message message, ITelegramBotClient bot) =>
  {
    if (message.Text is not { } messageText) return;

    var chatId = message.Chat.Id;

    Console.WriteLine($@"Received message: {messageText}");
    /* Handle spam and foul language */
    bool hasForbiddenContent = ForbiddenController.HasForbiddenContent(messageText);
    if (hasForbiddenContent)
    {
      await bot.DeleteMessageAsync(chatId, message.MessageId);
    }

    if (messageText.StartsWith("/") && messageText.Length > 1)
    {
      var command = messageText[1..]; /* Removes the '/' */
      var commandNotFound = !CommandAction.HasCommand(command);

      if (commandNotFound) return;

      if (PermissionController.CanRunCommand(chatId, command))
      {
        await CommandAction.RunCommand(command, bot, chatId);
      }

    }
    else
    {
      /* TODO: DuplicateQuestio, Suggested messages etc. */
    }
  };
}