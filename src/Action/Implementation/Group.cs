using Telegram.Bot;

namespace XTelegramBOT.Action;

public static class Group {
  public static CommandAction.CommandActionDelegate ListAll = async (ITelegramBotClient bot, long id) => {
    await bot.SendTextMessageAsync(id, "TODO: Elenco completo dei gruppi disponibili");
  };
}