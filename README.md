# XTelegramBOT Library
Wrapper della libreria .NET [Telegram.Bot](https://github.com/TelegramBots/Telegram.Bot)

> La documentazione non è aggiornata! Guardare versione git precedente.

## Aggiungere un comando 
La logica per la gestione dei comandi vede il parsing dei comandi elencati all'interno del file JSON [`/persistence/commandsInformation.json`](/persistence/commandsInformation.json) (implementazione: [`Configuration.cs`](/Configuration.cs) alla riga 18) in `IEnumerable<BotCommand>`. L'elenco dei comandi disponibli viene specificato all'interno del file [`XTelegramBOT.cs`](/src/XTelegramBOT.cs) all'interno dei metodi per la generazione di istanze (implementazione: [`XTelegramBOT.cs:Instance`](/src/XTelegramBOT.cs) alle righe 14 e 21) tramite la chiamata all'API `TelegramBot.SetMyCommandsAsync(IEnumerable<BotCommand> commands)`

## Helper comandi
Il nome e la descrizione dei comandi disponibili viene carciato dal file JSON [`/persistence/commandsInformation.json`](/persistence/commandsInformation.json):
```json
[
  {
    "Command": "help",
    "Description": "List of the commands descriptions and specifications"
  },
  {
    "Command": "hello",
    "Description": "Message greeting"
  }
]
```
Il nome del comando deve essere riportato in lowecase, caso contrario, viene l'anciata l'eccezione:
```sh
Unhandled exception. System.AggregateException: One or more errors occurred. (Bad Request: BOT_COMMAND_INVALID) ---> Telegram.Bot.Exceptions.ApiRequestException: Bad Request: BOT_COMMAND_INVALID
```

### Funzionalità comando
L'implementazion e l'esecuzione dei comandi avviene mediante l'impiego di dizionari le cui chaivi sono stringhe, ossia il nome del comando, e i valori sono `Func<Task>`, funzioni di callback contenenti la logica del comando.

Per comprendere il perché di questa scelta:
```cs
var COMMANDS_ACTION = new Dictionary<string, Func<Task>>() {
  { "help", async () => await Commands.ListHelpAsync(botClient, chatId) },
  { "names", async () => await Commands.ListNamesAsync(botClient, chatId) },
  { "groups", async () => await Commands.ListGroupsAsync(botClient, chatId) }
};

if (!messageText.StartsWith("/") || messageText.Length < 2) return;

var command = messageText[1..]; /* Removes the '/' */
try
{
  await COMMANDS_ACTION[command]();
}
catch (KeyNotFoundException ex)
{
  var hasCommand = Configuration.COMMANDS.Any(command.Equals);
  if (hasCommand) await Commands.CommandNotImplemented(botClient, chatId);
  else await Commands.CommandNotFound(botClient, chatId);
  Console.WriteLine(ex.Message);
}
```
La scelta progettuale elencata permette di identificare, modificare, aggiungere ed eliminare le facilmente le funzionalità e i comandi. 

# Troubleshooting
Nel caso in cui il `.gitignore` non funzioni:
```sh
git rm -rf --cached .
git add .
```