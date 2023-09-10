# XTelegramBOT Library
Wrapper della libreria .NET [Telegram.Bot](https://github.com/TelegramBots/Telegram.Bot)

# Setup
Creazione BOT Telegram: [documentazione Telegram](https://core.telegram.org/bots/#how-do-i-create-a-bot), [Tutorial](https://core.telegram.org/bots/tutorial)

Creare il file [./persistence/appsettings.json](./persistence/appsettings.json):
```json
{
  "Secrets": {
    "BotToken": "{BOT_TOKEN}"
  }
}
```

## Aggiungere un comando 
La logica per la gestione dei comandi vede il parsing dei comandi elencati all'interno del file JSON [`/persistence/commandsInformation.json`](/persistence/commandsInformation.json) (implementazione: [`Configuration.cs`](/Configuration.cs) alla riga 15) in `IEnumerable<BotCommand>`. L'elenco dei comandi disponibli viene specificato all'interno del file [/src/XTelegramBOT.cs](/src/XTelegramBOT.cs) all'interno dei metodi per la generazione di istanze (implementazione: [`XTelegramBOT.cs:Instance`](/src/XTelegramBOT.cs) alle righe 13 e 20) tramite la chiamata all'API `TelegramBot.SetMyCommandsAsync(IEnumerable<BotCommand> commands)`

## Helper comandi
Il nome e la descrizione dei comandi disponibili viene carciato dal file JSON [`/persistence/commandsInformation.json`](/persistence/commandsInformation.json):
```json
[
  {
    "Command": "nome_comando",
    "Description": "Descrizione comando: appare come preview durante la scelta del comando nella chat"
  },
]
```
Il nome del comando deve essere riportato in lowecase, caso contrario, viene l'anciata l'eccezione:
```sh
Unhandled exception. System.AggregateException: One or more errors occurred. (Bad Request: BOT_COMMAND_INVALID) ---> Telegram.Bot.Exceptions.ApiRequestException: Bad Request: BOT_COMMAND_INVALID
```

### Comandi
I comandi che vengono eseguiti sono memorizzati all'interno di in dizionario le cui chaivi sono stringhe, ossia l'identificativo del comando (`/help` etc.), e i valori sono metodi delegati che eseguono determinate Task (vedi [./src/Delegate.cs](./src/Delegate.cs)):

```cs
public delegate Task BotCommandActionAsync(XTelegramBOT botClient, long chatId);
```

Esiste un dizionario contententi dei comandi di default (vedi [./src/Action/BotCommandAction.cs](./src/Action/BotCommandAction.cs)):
```cs
public static Dictionary<string, BotCommandActionAsync> commandActions = new() {
  { "help", ListHelpAsync },
  { "names",ListNamesAsync },
  { "groups", ListGroupsAsync }
};
```
I metodi riportati sono asincroni e seguono la firma del delegato.

I comandi possono essere inseriti via dependency injection tramite construttore: (vedi [./Example/ExampleUpdateHandler.cs](./Example/ExampleUpdateHandler.cs))
```cs
public class ExampleUpdateHandler : IUpdateHandler
{
  private readonly Dictionary<Telegram.Bot.Types.Enums.MessageType, BotMessageHandlerActionAsync> MESSAGE_HANDLERS;
  private readonly Dictionary<string, BotCommandActionAsync> COMMAND_ACTIONS;

  public ExampleUpdateHandler(
    Dictionary<Telegram.Bot.Types.Enums.MessageType,
    BotMessageHandlerActionAsync> messageHandlers,
    Dictionary<string, BotCommandActionAsync> commandActions
  ) 
  {
    MESSAGE_HANDLERS = messageHandlers;
    COMMAND_ACTIONS = commandActions;
  }
  ...
}
```

L'esecuzione dei comandi avviene nel seguente modo (vedi il metodo `HandleCommandAsync` alla riga 43 del file [./src/Action/BotMessageHandlerAction.cs](./src/Action/BotMessageHandlerAction.cs)):

```cs
private static async Task HandleCommandAsync(
  XTelegramBOT botClient,
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
      await CommandNotFound(botClient, chatId);
    }
    Console.WriteLine(ex.Message);
  }
}
```

# Troubleshooting
Nel caso in cui il `.gitignore` non funzioni:
```sh
git rm -rf --cached .
git add .
```