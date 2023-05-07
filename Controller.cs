using Mohostani;
using System;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.RequestParams;
using VkNet.Model;
using System.Xml;
using VkNet.Enums;
using System.Data.SqlClient;

namespace moxbot
{
    internal class Controller
    {
        
        public static void MessageToChat(string UserMessage, long? peerId, SqlConnection sqlConnection) // отправка текстовых сообщений по команде
        {
                if (SqlManager.IsExist(sqlConnection, $"SELECT COUNT(*) as count FROM Command WHERE Command = N'{UserMessage}'"))
                {
                    string command = SqlManager.GetString(sqlConnection, $"SELECT Message FROM Command WHERE Command = N'{UserMessage}'");

                    var message = new MessagesSendParams

                    {
                        Message = command,

                        PeerId = peerId,
                        RandomId = 0,

                        Intent = Intent.Default


                    };

                    Program.api.Messages.SendAsync(message);
                }
        
        }

        public static void StickerToChat(string UserMessage, long? peerId) // отправка текстовых сообщений по команде
        {
            Program program = new Program();

            if (UserMessage == "олег")

            {

                var message = new MessagesSendParams

                {
                    StickerId = 76104,

                    PeerId = peerId,
                    RandomId = 0,

                    Intent = Intent.Default

                };

                Program.api.Messages.SendAsync(message);

            }
        }

        public static void Roullete(string UserMessage, long? peerId) // выбор случайного пользователя по команде
        {
            if (UserMessage == "кто мох")
            {
                var randomuser = Program.api.Messages.GetConversationMembers((long)peerId);

                Random rnd = new Random();


                var message = new MessagesSendParams

                {
                    Message = randomuser.Profiles[rnd.Next(0, randomuser.Profiles.Count)].FirstName,

                    PeerId = peerId,
                    RandomId = 0,

                    Intent = Intent.Default


                };

                Program.api.Messages.SendAsync(message);

            }
        }

        public static void TextAniimation(string UserMessage, long? peerId, long? ChatMessageId)
        {
            if (UserMessage == "грузи")

            {

                var message = new MessagesSendParams

                {
                    Message = "ОК",

                    PeerId = peerId,
                    RandomId = 0,

                    Intent = Intent.Default


                };

                var g = Program.api.Messages.Send(message);

                for (int i = 0; i < 4; i++)
                {

                    Program.api.Messages.Edit(new MessageEditParams
                    {
                        Message = "Загрузка",

                        ConversationMessageId = ChatMessageId + 1,
                        PeerId = (long)peerId,
                    });

                    Thread.Sleep(500);

                    Program.api.Messages.Edit(new MessageEditParams
                    {
                        Message = "Загрузка.",

                        ConversationMessageId = ChatMessageId + 1,
                        PeerId = (long)peerId,

                    });
                    Thread.Sleep(500);
                    Program.api.Messages.Edit(new MessageEditParams
                    {
                        Message = "Загрузка..",

                        ConversationMessageId = ChatMessageId + 1,

                        PeerId = (long)peerId,

                    });
                    Thread.Sleep(500);
                    Program.api.Messages.Edit(new MessageEditParams
                    {
                        Message = "Загрузка...",

                        ConversationMessageId = ChatMessageId + 1,

                        PeerId = (long)peerId,

                    });
                    Thread.Sleep(500);
                }
                Program.api.Messages.Edit(new MessageEditParams
                {
                    Message = "О",

                    ConversationMessageId = ChatMessageId + 1,
                    PeerId = (long)peerId,
                });

                Thread.Sleep(500);

                Program.api.Messages.Edit(new MessageEditParams
                {
                    Message = "ОЛ",

                    ConversationMessageId = ChatMessageId + 1,

                    PeerId = (long)peerId,
                });

                Thread.Sleep(500);

                Program.api.Messages.Edit(new MessageEditParams
                {
                    Message = "ОЛЕ",

                    ConversationMessageId = ChatMessageId + 1,

                    PeerId = (long)peerId,
                });

                Thread.Sleep(500);

                Program.api.Messages.Edit(new MessageEditParams
                {
                    Message = "ОЛЕГ",

                    ConversationMessageId = ChatMessageId + 1,

                    PeerId = (long)peerId,
                });

                Thread.Sleep(500);

                    Program.api.Messages.Edit(new MessageEditParams
                {
                    Message = "ОЛЕГ М",

                    ConversationMessageId = ChatMessageId + 1,

                    PeerId = (long)peerId,
                });

                Thread.Sleep(500);

                Program.api.Messages.Edit(new MessageEditParams
                {
                    Message = "ОЛЕГ МО",

                    ConversationMessageId = ChatMessageId + 1,

                    PeerId = (long)peerId,

                });
                Thread.Sleep(500);
                Program.api.Messages.Edit(new MessageEditParams
                {
                    Message = "ОЛЕГ МОХ",

                    ConversationMessageId = ChatMessageId + 1,

                    PeerId = (long)peerId,

                });
            }
        }

        public static void СonfirmMessage(string UserMessage, long? peerId, SqlConnection sqlConnection)
        {
            try
            {
                Random rand = new Random();

                if (UserMessage[0] == 'я' && UserMessage[1] == ' ')

                {
                    string reply = UserMessage.TrimStart('я');
                    int countOfMessages = SqlManager.CountOfLines(sqlConnection, "SELECT COUNT(DISTINCT id) as count FROM Confirm");
                    string confirmMessage = SqlManager.GetString(sqlConnection, $"SELECT Message FROM Confirm WHERE id = {rand.Next(1, countOfMessages)}");

                    var message = new MessagesSendParams

                    {
                        Message = $"{confirmMessage}{reply} 🤔🤔🤔",

                        PeerId = peerId,
                        RandomId = 0,

                        Intent = Intent.Default


                    };

                    Program.api.Messages.SendAsync(message);

                }
            }
            catch 
            {

               Program.LongPoll();
            }
 
        }

        public static void SeparateMessage(string UserMessage, long? peerId)
        {


            if (UserMessage == "олег сегодня мох?")
                
            {
                for (int i = 0; i < 5; i++)
                {
                    var date = DateTime.Now;
                    string sepMessage = "ЕЩЁ КАКОЙ! ЯБ ЕМУ В СУП МЫШЬЯК ЗАКИНУЛ";
                    var message = new MessagesSendParams

                    {
                        Message = sepMessage,

                        PeerId = peerId,
                        RandomId = 0,

                        Intent = Intent.Default
                        

                    };

                    Program.api.Messages.SendAsync(message);
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
                    var message = new MessagesSendParams

                    {
                        Message = item.Text,

                        PeerId = peerId,
                        RandomId = 0,

                        Intent = Intent.Default

                    };

                    Program.api.Messages.SendAsync(message);
 
                }
              

            }


        }
    }
}
