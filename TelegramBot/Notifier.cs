using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot
{
    public class Notifier
    {
        private static TelegramBotClient BotClient = new("5772439890:AAFD5qLgN5K_Pcz_N6NGJF7ZES0XAQE3y1c");
        private static long ChatId = -1001867813618;
        

        private static Notifier telegramNotifier;
        public static Notifier Instance => telegramNotifier ??= new Notifier();


        /// <summary>
        /// Send Message to Telegram Chat
        /// </summary>
        /// <param name="messageBody">Message body</param>
        /// <param name="disableNotification"></param>
        /// <returns>Message id</returns>
        public int Notify(string messageBody, bool disableNotification = true)
        {
            try
            {
                long chatId = ChatId;

                var message = BotClient.SendTextMessageAsync(chatId, $"{messageBody}", parseMode: ParseMode.Html, disableNotification: disableNotification).Result;

                return message.MessageId;
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Data}\n{e.Message}\n{e.StackTrace}");
                return -1;
            }
        }
    }
}