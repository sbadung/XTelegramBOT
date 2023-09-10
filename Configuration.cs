using Telegram.Bot.Types;
using XTelegramBOT.Utilities;

namespace XTelegramBOT;

public static class Configuration
{
    private static readonly string BASE_PATH = Environment.CurrentDirectory;
    public static readonly string FORBIDDEN_WORDS = Path.Combine(BASE_PATH, "persistence", "forbiddenWords.json");
    private static readonly string APP_SETTINGS = Path.Combine(BASE_PATH, "persistence", "appsettings.json");
    private static readonly string COMMANDS_INFORMATION = Path.Combine(BASE_PATH, "persistence", "commandsInformation.json");

    public static readonly string BOT_TOKEN = new JSONConfigurationLoader().LoadConfiguration(APP_SETTINGS).GetSection("Secrets:BotToken").Value!;

    public static IEnumerable<BotCommand> COMMANDS
    {
        get
        {
            var json = System.IO.File.ReadAllText(COMMANDS_INFORMATION);
            return System.Text.Json.JsonSerializer.Deserialize<IEnumerable<BotCommand>>(json) ?? new List<BotCommand>();
        }
    }
}