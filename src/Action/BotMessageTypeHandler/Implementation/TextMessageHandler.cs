using Telegram.Bot;
using Telegram.Bot.Types;
using XTelegramBOT.Utility;

namespace XTelegramBOT.Action.BotMessageTypeHandler.Implementation
{
    public class TextMessageHandler : AbstractBotMessageTypeHandler
  {
    public override async Task Handle(ITelegramBotClient botClient, Message message, Dictionary<string, IBotCommandFunctionality>? commandFunctionalities = null)
    {

      {
        /* TODO: Handle invalid text */
        if (message.Text is not { } messageText) return;

        var chatId = message.Chat.Id;

        /* Handle spam and foul language*/
        bool hasForbiddenContent = ForbiddenWordController.ContainsOffensiveWord(messageText) || ForbiddenDomainController.ContainsForbiddenDomain(messageText);
        if (hasForbiddenContent)
        {
          await botClient.DeleteMessageAsync(chatId, message.MessageId);
        }

        if (messageText.StartsWith("/") && messageText.Length > 1)
        {
          var command = messageText[1..]; /* Removes the '/' */
          await HandleCommandAsync(botClient, chatId, command, commandFunctionalities ?? Configuration.Default.COMMANDS_FUNCTIONALITIES);
        }
        else
        {
          /* TODO: DuplicateQuestion */
        }
      }
    }
  }
}