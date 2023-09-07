using Telegram.Bot;
using XTelegramBOT.Command;
using XTelegramBOT.Update;

namespace XTelegramBOT.Example
{
    public class ExampleUpdateHandler : IUpdateHandler
    {
        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Telegram.Bot.Types.Update update,
            CancellationToken cancellationToken)
        {
            /* Only considering text messages */
            if (update.Message is not { } message) return;
            if (message.Text is not { } messageText) return;
            /* To add functionality in the future for other message types
             * message.MessageType...
            */

            var chatId = message.Chat.Id;
            var username = message.Chat.Username;

            /* Any idea on how to decouple part and inject it into the class? Maybe turning actions in to tasks? */
            var COMMANDS_ACTION = new Dictionary<string, Func<Task>>()
            {
                { "help", async () => await Commands.ListHelpAsync(botClient, chatId) },
                { "names", async () => await Commands.ListNamesAsync(botClient, chatId) },
                { "groups", async () => await Commands.ListGroupsAsync(botClient, chatId) }
            };

            if (!messageText.StartsWith("/") || messageText.Length < 2) return;
            var command = messageText[1..]; /* Removes the '/' */
            try
            {
                await COMMANDS_ACTION[command]();
            }
            catch (KeyNotFoundException ex)
            {
                var hasCommand = Configuration.COMMANDS.Any(command.Equals);
                if (hasCommand) await Commands.CommandNotImplemented(botClient, chatId);
                else await Commands.CommandNotFound(botClient, chatId);
                Console.WriteLine(ex.Message);
            }
        }
    }
}