using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace MyTankInTelegram
{
    class Program
    {
        static ITelegramBotClient botClient;
        static WebClient wc = new WebClient();
        static void Main(string[] args)
        {
            botClient = new TelegramBotClient("1764742240:AAGXsEibbcwFDZeE58afc_B_xQ4zS8sXlU8");
            var me = botClient.GetMeAsync().Result;
            Console.WriteLine($"{me.Id}, {me.Username}");


            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();
            Thread.Sleep(int.MaxValue);
        }

        private static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            if(e.Message.Text != null)
            {
                string helpDescription = "У нас есть команды -help\nDookus\n и остальные";
                Console.WriteLine($"{e.Message.Chat.Id} and USerNmae {e.Message.Chat.FirstName} {e.Message.Chat.LastName}");

                //Switch block
                switch (e.Message.Text)
                {
                    case string message when message == "help":
                        await SendMessage(e.Message.Chat, helpDescription);
                        break;
                    case string message when message == "Dookus":
                        await SendMessage(e.Message.Chat, "Dookus - рак, пробития не будет сегодня");
                        break;
                    default:
                        await SendMessage(e.Message.Chat, "Напиши help, если руки из жопы");
                        break;

                }
            }
        }

      

        private static async Task SendMessage(Chat chatId, string message)
        {
            await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: message
                                );
        }
    }
}
