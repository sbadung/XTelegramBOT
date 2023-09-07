using System.Text.Encodings.Web;
using static System.Text.Encodings.Web.HtmlEncoder;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace XTelegramBOT.Command;

public static class Commands
{
    private static string FormatCommandsHTML(IEnumerable<BotCommand> commands)
    {
        var html = $@"";
        commands.ToList().ForEach(command => html += @$"/{command.Command}: {command.Description}" + '\n');
        return html;
    }

    public static async Task ListHelpAsync(ITelegramBotClient botClient, long chatId)
    {
        var response = FormatCommandsHTML(Configuration.COMMANDS);
        await botClient.SendTextMessageAsync(chatId, response);
    }

    public static async Task ListNamesAsync(ITelegramBotClient botClient, long chatId)
    {
        /* TODO: Leggere i nomi da un file JSON */
        var names = new List<string> { "John", "Alice", "Bob", "Eve" };
        var response = "List of names:\n" + string.Join("\n", names);

        await botClient.SendTextMessageAsync(chatId, response);
    }

    public static async Task ListGroupsAsync(ITelegramBotClient botClient, long chatId)
    {
        /* TODO: Leggere i gruppi da un file JSON */
        var groups = new List<string> { "Ingegneria", "Design", "Architettura" };
        var response = "List of groups:\n" + string.Join("\n", groups);

        await botClient.SendTextMessageAsync(chatId, response);
    }

    public static async Task CommandNotImplemented(ITelegramBotClient botClient, long chatId)
    {
        var responseContent = "Work in progress, this command will be implemented soon";
        await botClient.SendTextMessageAsync(chatId, responseContent);
    }

    public static async Task CommandNotFound(ITelegramBotClient botClient, long chatId)
    {
        var response = "Unrecognized command. Say what?";
        await botClient.SendTextMessageAsync(chatId, response);
    }
}