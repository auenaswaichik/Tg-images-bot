using System.Data;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using MyProj.DBContext;
using MyProj.Entities;
using Microsoft.IdentityModel.Tokens;

namespace MyProj.Evenst;
public static class UpdateHandler {

    public static async Task UpdateStatus(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken) {

        try {

            switch (update.Type) {

                case UpdateType.Message: {

                    var message = update.Message;
                    var user = message.From;
                    var chat = message.Chat;

                    var messagePhoto = message.Photo;

                    if (messagePhoto is not null) {

                        addImageId(message);

                        return;

                    }
                    if (message.Text == "/send_image") {
                        
                        sendImage(chat, botClient);
                        
                        return; 

                    }

                    sendErrorMessage(chat, botClient);

                    return;

                }

            }

        }
        catch (Exception ex) {

            Console.WriteLine(ex.ToString());

        }

    }

    private static void addImageId(Message message) {

        var fileId = message.Photo.FirstOrDefault().FileId;
        var fromWho = message.From.FirstName;

        using(var db = new ApplicationContext()) {

            var imageInfo = new ImageInfo(fileId, fromWho);

            var im = db.Images.Where(x => x.FileId == imageInfo.FileId) ;

            if (im.IsNullOrEmpty())
                db.Images.Add(imageInfo);

            db.Images.Remove(db.Images.FirstOrDefault(x => x.FromWho == "Максим"));

            db.SaveChanges();

        }       

    }

    private static void sendImage(Chat chat, ITelegramBotClient bot) {

        var r = new Random();
        
        using(var db = new ApplicationContext()) {
            var imageInfo = db.Images.ElementAt(r.Next(db.Images.OrderBy(x => x.Id).Last().Id));
            bot.SendPhoto(
                chat.Id, 
                imageInfo.FileId,
                "Message from " + imageInfo.FromWho);

        }


    }

    private static void sendErrorMessage (Chat chat, ITelegramBotClient bot) {

        bot.SendMessage (

            chat.Id,
            "Your message is incorrect.\n" 
            + "Print '/send_image' or send your image."

        );

    }


};
