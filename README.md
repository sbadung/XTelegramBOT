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
1. Migrare i comandi base dalla repository princiaple
2. Aggiungere gestione automatica dei messaggi ricevuti: per semplificare, non consentiamo l'invio di altre forme di media
3. Realizzare file JSON articolati
4. Rivedere la logistica per l'aggiunta del BOT nei vari gruppi
5. Scrivere la documentazione completa del codice, spiegando le scelte di progetto e il perché di determinati pattern

# Troubleshooting
Nel caso in cui il `.gitignore` non funzioni:
```sh
git rm -rf --cached .
git add .
```