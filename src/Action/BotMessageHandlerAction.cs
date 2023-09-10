using Telegram.Bot;
using Telegram.Bot.Types;

namespace XTelegramBOT.Action
{
    public static class BotMessageHandlerAction
    {
        public static async Task HandleUnknownMessageAsync(
        ITelegramBotClient botClient,
        Message message,
        Dictionary<string, BotCommandActionAsync>? commandActions = null
    )
        {
            var response = $"Unknow message type received";
            var chatId = message.Chat.Id;
            await botClient.SendTextMessageAsync(chatId, response);
        }

        public static async Task HandleTextMessageAsync(
            ITelegramBotClient botClient,
            Message message,
            Dictionary<string, BotCommandActionAsync>? commandActions = null
        )
        {
            /* TODO: Handle invalid text */
            if (message.Text is not { } messageText) return;

            var chatId = message.Chat.Id;
            var username = message.Chat.Username;

            if (!messageText.StartsWith("/") || messageText.Length < 2) return;
            var command = messageText[1..]; /* Removes the '/' */
            try
            {
                if (commandActions is null) return; /*Handle null ses */
                await commandActions[command](botClient, chatId);
            }
            catch (KeyNotFoundException ex)
            {
                var hasCommand = Configuration.COMMANDS.Any(command.Equals);
                if (hasCommand) await CommandNotImplemented(botClient, chatId);
                else await CommandNotFound(botClient, chatId);
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