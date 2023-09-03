using XTelegramBOT.Utilities;

namespace XTelegramBOT;
public static class Configuration
{
    private static readonly string BASE_PATH = Environment.CurrentDirectory;
    public static readonly string FORBIDDEN_WORDS = Path.Combine(BASE_PATH, "persistence", "forbiddenWords.json");
    public static readonly string APP_SETTINGS = Path.Combine(BASE_PATH, "persistence", "appsettings.json");
    public static readonly string COMMANDS_INFORMATION = Path.Combine(BASE_PATH, "persistence", "commandsInformation.json");
    public static readonly string BOT_TOKEN = new JSONConfigurationLoader().LoadConfiguration(Configuration.APP_SETTINGS).GetSection("Secrets:BotToken").Value!;
    public static List<Telegram.Bot.Types.BotCommand> COMMANDS
    {
        get
        {
            string json = System.IO.File.ReadAllText(Configuration.COMMANDS_INFORMATION);
            return System.Text.Json.JsonSerializer.Deserialize<List<Telegram.Bot.Types.BotCommand>>(json) ?? new List<Telegram.Bot.Types.BotCommand>();
        }
    }
}
