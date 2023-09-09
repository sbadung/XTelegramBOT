using Telegram.Bot;
using Telegram.Bot.Types;
using XTelegramBOT.Command;
using XTelegramBOT.Update;

namespace XTelegramBOT.Example
{
    public class ExampleUpdateHandler : IUpdateHandler
    {
        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Telegram.Bot.Types.Update update, CancellationToken cancellationToken)
        {
            if (update == null || update.Message == null) return;
            var message = update.Message;
            var UPDATE_HANDLERS = new Dictionary<Telegram.Bot.Types.Enums.MessageType, Func<Task>>() {
                { Telegram.Bot.Types.Enums.MessageType.Text, async () => await HandleTextMessageAsync(botClient, message) }
            };

            try
            {
                await UPDATE_HANDLERS[update.Message.Type]();
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static async Task HandleTextMessageAsync(ITelegramBotClient botClient, Message message)
        {
            /* TODO: Handle invalid text */
            if (message.Text is not { } messageText) return;



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