using System.Data;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace MyProj.Evenst;

public static class ErrorHandler {

    public static Task ErrorStatus(ITelegramBotClient botClient, Exception error, CancellationToken cancellationToken) {

        var ErrorMessage = error switch {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => error.ToString()
        };

        Console.WriteLine(ErrorMessage);
        return Task.CompletedTask;

    }

};
