using Mohostani;
using System;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.RequestParams;
using VkNet.Model;

namespace moxbot
{
    internal class Controller
    {
        
        public static void MessageToChat(string UserMessage, long? peerId) // отправка текстовых сообщений по команде
        {

            if (UserMessage == "я")

            {
                var message = new MessagesSendParams

                {
                    Message = "крутой",

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

        public static void СonfirmMessage(string UserMessage, long? peerId)
        {
            try
            {
                if (UserMessage[0] == 'я' && UserMessage[1] == ' ')

                {
                    string reply = UserMessage.Replace("я ", "");

                    var message = new MessagesSendParams

                    {
                        Message = $"Капец он реально {reply} 😮😮😮",

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
    }
}
