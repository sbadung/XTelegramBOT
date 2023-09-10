using Telegram.Bot;
using XTelegramBOT.Update;

namespace XTelegramBOT.Example
{
    public class ExampleUpdateHandler : IUpdateHandler
    {
        private readonly Dictionary<Telegram.Bot.Types.Enums.MessageType, BotMessageHandlerActionAsync> MESSAGE_HANDLERS;
        private readonly Dictionary<string, BotCommandActionAsync> COMMAND_ACTIONS;

        public ExampleUpdateHandler(
            Dictionary<Telegram.Bot.Types.Enums.MessageType, BotMessageHandlerActionAsync> messageHandlers,
            Dictionary<string, BotCommandActionAsync> commandActions
        ) {
            MESSAGE_HANDLERS = messageHandlers;
            COMMAND_ACTIONS = commandActions;
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Telegram.Bot.Types.Update update, CancellationToken cancellationToken)
        {
            if (update == null || update.Message == null) return;
            var message = update.Message;

            try
            {
                await MESSAGE_HANDLERS[update.Message.Type](botClient, message, COMMAND_ACTIONS);
            }
            catch (KeyNotFoundException ex)
            {
                /* TODO: Aggiugnere Logger */
                Console.WriteLine(ex.Message);
            }
        }
    }
}

