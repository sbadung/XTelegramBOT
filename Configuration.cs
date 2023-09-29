using XTelegramBOT.Action.BotMessageTypeHandler;
using XTelegramBOT.Action.BotMessageTypeHandler.Implementation;
using XTelegramBOT.BotCommandFunctionality.Implementation;
using XTelegramBOT.Utilities;

namespace XTelegramBOT;

public static class Configuration
{
    private static readonly string BASE_PATH = Environment.CurrentDirectory;
    public static readonly string FORBIDDEN_WORDS_PATH = Path.Combine(BASE_PATH, "persistence", "forbiddenWords.json");
    public static readonly string FORBIDDEN_DOMAINS_PATH = Path.Combine(BASE_PATH, "persistence", "forbiddenDomains.json");
    private static readonly string APP_SETTINGS_PATH = Path.Combine(BASE_PATH, "persistence", "appsettings.json");
    private static readonly string COMMANDS_INFORMATION_PATH = Path.Combine(BASE_PATH, "persistence", "commandsInformation.json");

    public static class Default
    {
        public static readonly string BOT_TOKEN = new JSONConfigurationLoader().LoadConfiguration(APP_SETTINGS_PATH).GetSection("Secrets:BotToken").Value!;

        public static List<string> FORBIDDEN_WORDS
        {
            get
            {
                var json = File.ReadAllText(FORBIDDEN_WORDS_PATH);
                return System.Text.Json.JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
            }
        }

        public static List<string> FORBIDDEN_DOMAINS
        {
            get
            {
                var json = File.ReadAllText(FORBIDDEN_DOMAINS_PATH);
                return System.Text.Json.JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
            }
        }

        public static IEnumerable<Telegram.Bot.Types.BotCommand> BOT_COMMANDS
        {
            get
            {
                var json = File.ReadAllText(COMMANDS_INFORMATION_PATH);
                return System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Telegram.Bot.Types.BotCommand>>(json) ?? new List<Telegram.Bot.Types.BotCommand>();
            }
        }

        public static readonly Dictionary<string, IBotCommandFunctionality> COMMANDS_FUNCTIONALITIES = new() {
            { "groups", new ListGroups() }
        };

        public static readonly Dictionary<Telegram.Bot.Types.Enums.MessageType, IBotMessageTypeHandler> MESSAGE_TYPE_HANDLERS = new() {
            { Telegram.Bot.Types.Enums.MessageType.Text,  new TextMessageHandler() }
        };
    }
}