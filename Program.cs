using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using MyProj.Evenst;

var botClient = new TelegramBotClient("7778010054:AAGmAqG1jYe80deKq3s787qpMuNTcTYRLpo");
var receiverOptions = new ReceiverOptions {

    AllowedUpdates = new[] {

        UpdateType.Message,

    },

};

using var cts = new CancellationTokenSource();

botClient.StartReceiving(UpdateHandler.UpdateStatus, ErrorHandler.ErrorStatus, receiverOptions, cts.Token);

Console.WriteLine("Gotovo");

await Task.Delay(-1);
