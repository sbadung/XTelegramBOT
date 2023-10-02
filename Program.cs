using XTelegramBOT.Action.BotMessageTypeHandler;
using XTelegramBOT.Action.BotMessageTypeHandler.Implementation;
using XTelegramBOT.BotCommandFunctionality.Implementation;
using XTelegramBOT.Example;
using XTelegramBOT.Polling;
using XTelegramBOT.Update;

namespace XTelegramBOT;

internal static class Program
{

  public static readonly Dictionary<string, IBotCommandFunctionality> COMMANDS_FUNCTIONALITIES = new() {
    { "groups", new ListGroups() }
  };
  
  public static readonly Dictionary<Telegram.Bot.Types.Enums.MessageType, IBotMessageTypeHandler> MESSAGE_TYPE_HANDLERS = new() {
    { Telegram.Bot.Types.Enums.MessageType.Text, new TextMessageHandler() }
  };

  private static async Task Main()
  {
    string BOT_TOKEN = "";
    if (string.IsNullOrEmpty(BOT_TOKEN)) BOT_TOKEN = Configuration.Default.BOT_TOKEN;

    IUpdateHandler updateHandler = new ExampleUpdateHandler(MESSAGE_TYPE_HANDLERS, COMMANDS_FUNCTIONALITIES);
    IPollingErrorHandler pollingErrorHandler = new ExamplePollingErrorHandler();

    var bot = XTelegramBOT.Instance(BOT_TOKEN).Result;
    await bot.RunAsync(updateHandler, pollingErrorHandler);
  }
}

/* 
  // TODO: Creare JSON file separati per la lettura e scrittura dei diversi utenti (ID Telegram) con privilegi e restrizioni
  // Mantenere il file privato, non esporre nella repository
  public static List<int> CREATORS = new () { 12345678, 87654321 }

  public static readonly Dictionary<string, IBotCommandFunctionality> COMMANDS_FUNCTIONALITIES = new() {
    { "groups_list", CommandDispatcher.SendRecommendedGroupsAsync },
    { "groups_search", Groups.SendGroupsByTitle }
  };

  public static List<Dictionary<string, IBotCommandFunctionality> CREATOR_COMMANDS_FUNCTIONALITIES = new () {
    { "link_generate", CommandDispatcher.ForceCheckInviteLinksAsync },
    { "reboot", RebootUtil.RebootWithLog },
    { "channel_message_send", SendMessage.SendMessageInChannel2 },
    { "bot_configuration", BotConfig.GetConfig2 }
    { "bot_groups", Groups.GetGroups}
    { "database_configuration", BotConfig.GetDbConfig }
  };

  public static List<Dictionary<string, IBotCommandFunctionality> CONTROLLER_COMMANDS_FUNCTIONALITIES = new () {
    { "user_mute", RestrictUser.MuteAllAsync },
    { "user_unmute", RestrictUser.UnMuteAllAsync },
    { "user_ban", RestrictUser.BanAllAsync },
    { "user_unban", RestrictUser.UnbanAllAsync },
    { "banned_user_messages_delete", RestrictUser.BanDeleteAllAsync },
    { "message_delete", RestrictUser.DeleteMessageFromUser},
    { "spam_test", CommandDispatcher.TestSpamAsync },
  };

*/