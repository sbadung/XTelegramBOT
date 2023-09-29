using Telegram.Bot;
using XTelegramBOT.Action.BotMessageTypeHandler;
using XTelegramBOT.Update;

namespace XTelegramBOT.Example
{
    public class ExampleUpdateHandler : IUpdateHandler
    {
        private readonly Dictionary<Telegram.Bot.Types.Enums.MessageType, IBotMessageTypeHandler> _messageTypeHandlers;
        private readonly Dictionary<string, IBotCommandFunctionality> _commandFunctionalities;

        public ExampleUpdateHandler(
            Dictionary<Telegram.Bot.Types.Enums.MessageType, IBotMessageTypeHandler> messageTypeHandlers,
            Dictionary<string, IBotCommandFunctionality> commandFunctionalities
        )
        {
            _messageTypeHandlers = messageTypeHandlers;
            _commandFunctionalities = commandFunctionalities;
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Telegram.Bot.Types.Update update, CancellationToken cancellationToken)
        {
            if (update == null || update.Message == null) return;
            var message = update.Message;

            try
            {
                await _messageTypeHandlers[update.Message.Type].Handle(botClient, message, _commandFunctionalities);
            }
            catch (KeyNotFoundException ex)
            {
                /* TODO: Aggiugnere Logger */
                Console.WriteLine(ex.Message);
            }
        }
    }
}

