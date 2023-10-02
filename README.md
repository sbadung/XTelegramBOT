# XTelegramBOT Library
Wrapper della libreria .NET [Telegram.Bot](https://github.com/TelegramBots/Telegram.Bot)

## Setup
Creazione BOT Telegram: [documentazione Telegram](https://core.telegram.org/bots/#how-do-i-create-a-bot), [Tutorial](https://core.telegram.org/bots/tutorial)

Creare il file [./persistence/appsettings.json](./persistence/appsettings.json):
```json
{
  "Secrets": {
    "BotToken": "{BOT_TOKEN}"
  }
}
```

## Todo
1. Migrare e migliorare i comandi base dalla repository princiaple: https://github.com/PoliNetworkOrg/PoliNetworkBot_CSharp/blob/master/PoliNetworkBot_CSharp/Code/Bots/Moderation/Dispatcher/SwitchDispatcher.cs
2. Realizzare file JSON articolati: utenti con permessi, utenti con restrizioni, descrizioni divise per lingua, configurazioni
3. Scrivere la documentazione completa del codice, spiegando le scelte di progetto e il perché di determinati pattern

### File JSON
`commands.json`:
```json
{
  "commands": [ "unique_name" ]
}
```

`description_it.json`:
```json
{
  "description": [ 
    {
      "command": "unique_name",
      "description": "Descrizione nella lingua indicata dopo il trattino _" 
    }  
  ]
}
```

`description_en.json`:
```json
{
  "description": [ 
    {
      "command": "unique_name",
      "description": "Description in the indicated language after the underscore _" 
    }  
  ]
}
```
In questo modo, qual'ora si volesse aggiungere una nuova lingua, basta aggiungere un nuovo file: `description_language.json`


`users.json`
```json
{
  "owners": [ 1234, 2345, 3456 ],
  "banned": [ 0000, 1111, 2222 ],
  "muted":  [ 0101, 1212, 2323 ],
  "admin":  [ 7777, 6666, 8888 ]
}
```

```cs
var ADMINS = loadAdmins("users.json");
var ADMINS_COMMANDS = loadAdminsCommandsInformation("commands.json");
...

var hasPermission = ADMINS_COMMANDS.Contains(command) && ADMINS.Contains(chatId);
if (hasPermission) 
{
  await ADMINS_COMMANDS_FUNCTIONALITY[command].Run(botClient, chatId);
}
```

# Troubleshooting
Nel caso in cui il `.gitignore` non funzioni:
```sh
git rm -rf --cached .
git add .
```