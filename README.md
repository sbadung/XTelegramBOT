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
# Troubleshooting
Nel caso in cui il `.gitignore` non funzioni:
```sh
git rm -rf --cached .
git add .
```