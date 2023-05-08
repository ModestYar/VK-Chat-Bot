using Mohostani;
using System;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.RequestParams;
using VkNet.Model;
using System.Xml;
using VkNet.Enums;
using System.Data.SqlClient;
using System.Collections.Specialized;

namespace moxbot
{
    internal class Controller
    {
        
        public static void MessageByCommand(string UserMessage, long? peerId, SqlConnection sqlConnection) // отправка текстовых сообщений по команде
        {
                if (SqlManager.IsExist(sqlConnection, $"SELECT COUNT(*) as count FROM Command WHERE Command = N'{UserMessage}'"))
                {
                    string command = SqlManager.GetString(sqlConnection, $"SELECT Message FROM Command WHERE Command = N'{UserMessage}'");

                    SendingManager.MessageToChat(command, peerId);
                }
        
        }

        public static void StickerByCommand(string UserMessage, long? peerId) // отправка текстовых сообщений по команде
        {
            Program program = new Program();

            if (UserMessage == "олег")

            {
                SendingManager.MessageToChat(76104, peerId);
            }
        }

        public static void Roullete(string UserMessage, long? peerId) // выбор случайного пользователя по команде
        {
            if (UserMessage != "" && UserMessage.Length > 4)
            {
                if (UserMessage[0] == 'к' && UserMessage[1] == 'т' && UserMessage[3] == ' ')
                {
                    string who = "кто";
                    
                    var randomuser = Program.api.Messages.GetConversationMembers((long)peerId);
                    string reply = UserMessage.TrimStart(who.ToCharArray()).TrimEnd('?');
                    Random rnd = new Random();
                    string name = randomuser.Profiles[rnd.Next(0, randomuser.Profiles.Count)].FirstName;

                    SendingManager.MessageToChat($"{name}{reply}", peerId);
                }
            }

        }

        public static void TextAniimation(string UserMessage, long? peerId, long? ChatMessageId, SqlConnection sqlConnection)
        {
            if (SqlManager.IsExist(sqlConnection, $"SELECT COUNT(*) as count FROM TextAnimationsCommands WHERE Command = N'{UserMessage}'"))

            {
                SendingManager.MessageToChat("OK", peerId);

                int countOfLines = SqlManager.CountOfLines(sqlConnection, "SELECT COUNT(DISTINCT id) as count FROM TextAnimations");

                string request = SqlManager.GetString(sqlConnection, $"SELECT Request FROM TextAnimationsCommands WHERE Command = N'{UserMessage}'");
                
                for (int i = 1; i <= countOfLines; i++)
                {
                    string message = SqlManager.GetString(sqlConnection, $"{request}{i}");
                    SendingManager.EditMessageIntoChat(message, peerId, ChatMessageId);
                    Thread.Sleep(1000);
                }
            }
        }

        public static void СonfirmMessage(string UserMessage, long? peerId, SqlConnection sqlConnection)
        {

            Random rand = new Random();
            if (UserMessage != "" && UserMessage.Length > 3)
            {

                if (UserMessage[0] == 'я' && UserMessage[1] == ' ')

                {
                    string reply = UserMessage.TrimStart('я');
                    int countOfMessages = SqlManager.CountOfLines(sqlConnection, "SELECT COUNT(DISTINCT id) as count FROM Confirm");
                    string confirmMessage = SqlManager.GetString(sqlConnection, $"SELECT Message FROM Confirm WHERE id = {rand.Next(1, countOfMessages)}");

                    SendingManager.MessageToChat($"{confirmMessage}{reply}", peerId);
                }
            }
      
        }

        public static void SeparateMessage(string UserMessage, long? peerId)
        {


            if (UserMessage == "олег сегодня мох?")
                
            {
                for (int i = 0; i < 5; i++)
                {
                    string sepMessage = "ЕЩЁ КАКОЙ! ЯБ ЕМУ В СУП МЫШЬЯК ЗАКИНУЛ";
                    SendingManager.MessageToChat(sepMessage, peerId);
                    Thread.Sleep(1000);
                }
            }
        }

        public static void RandomMessage(string UserMessage, long? peerId)
        {

            var rand = new Random();

            IEnumerable<ulong> MessageId = new List<ulong> { (ulong)rand.Next(741500,742000) };
            IEnumerable<string> listValues3 = new List<string> { "олег мох" };

            if (UserMessage == "давай")
            {
                var messageInfo = Program.api.Messages.GetByConversationMessageId((long)peerId, MessageId, listValues3);
                foreach (var item in messageInfo.Items)
                {
                    SendingManager.MessageToChat(item.Text, peerId);
                }
            }
        }
    }
}
