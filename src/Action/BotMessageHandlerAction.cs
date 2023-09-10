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

            if (messageText.StartsWith("/") && messageText.Length > 1)
            {
                var command = messageText[1..]; /* Removes the '/' */
                await HandleCommandAsync(botClient, chatId, command, commandActions ?? BotCommandAction.commandActions);
            }
            else
            {
                /* Filtra spam */
                /* Cerca domande duplicate */
                /* Altre funzionalità */
            }
        }

        private static async Task HandleCommandAsync(
            ITelegramBotClient botClient,
            long chatId,
            string command,
            Dictionary<string, BotCommandActionAsync> commandActions
        )
        {
            try
            {
                await commandActions[command](botClient, chatId);
            }
            catch (KeyNotFoundException ex)
            {
                /* La chiave utilizzata per identificare il comando non si trovare nel dizionario: */
                IEnumerable<BotCommand> commands = await botClient.GetMyCommandsAsync();
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
