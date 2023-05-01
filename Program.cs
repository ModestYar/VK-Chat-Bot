using System;
using VkNet;
using VkNet.Model;
using VkNet.Model.RequestParams;
using VkNet.Model.GroupUpdate;
using VkNet.Enums.SafetyEnums;
using Ini;
using moxbot;

namespace Mohostani
{
    class Program
    {
        static string[] lines = File.ReadLines("Config.ini").ToArray();
        static Dictionary<string, Ini.Category> result = Ini.Parser.Parse(lines);
        static Category BotConfig = result["BotConfig"];

        static string key = BotConfig.GetEntryByKey("Key").Value;
        static int groupId = Convert.ToInt32(BotConfig.GetEntryByKey("GroupID").Value);
        public static VkApi api = new VkApi();
        public string? UserMessage { get; set; }
        public long? PeerId { get; set; }
        public long? ChatMessageId { get; set; }

        
        static void Main(string[] args)
        {
            Auth();

            LongPoll(); 
        }
        public static void Auth() // авторизация
        {
            api.Authorize(new ApiAuthParams
            {
                AccessToken = key
            });
            Console.WriteLine($"ВОТ ТВОЙ ТОКЕН МУЖИК\t {api.Token} \t ПОХОДУ РОБИТ");
        } 


        public static void LongPoll() // методы отправки данных вынесены сюда
        {
      
            Program getUserMes = new Program();
            Program getPeerId = new Program();
            Program getChatMesId = new Program();


            while (true)
            {
                var s = api.Groups.GetLongPollServer(ulong.Parse(groupId.ToString()));

                var poll = api.Groups.GetBotsLongPollHistory(new BotsLongPollHistoryParams()
                {
                    Server = s.Server,

                    Ts = s.Ts,

                    Key = s.Key,

                    Wait = 25

                });

                //проверяем появилось ли новое событие

                if (poll?.Updates == null)
                {
                    continue;
                }
                //»копаемся» в новом событие

                foreach (var a in poll.Updates)
                {
                    if (a.Type.Value == GroupUpdateType.MessageNew)
                    {

                      getUserMes.UserMessage = ((MessageNew)a.Instance).Message.Text.ToLower();

                        getPeerId.PeerId = ((MessageNew)a.Instance).Message.PeerId;

                        getChatMesId.ChatMessageId = ((VkNet.Model.GroupUpdate.MessageNew)a.Instance).Message.ConversationMessageId;

                        Controller.MessageToChat (getUserMes.UserMessage, getPeerId.PeerId);

                        Controller.StickerToChat(getUserMes.UserMessage, getPeerId.PeerId);

                        Controller.Roullete(getUserMes.UserMessage, getPeerId.PeerId);

                        Controller.TextAniimation(getUserMes.UserMessage, getPeerId.PeerId, getChatMesId.ChatMessageId);

                        Controller.СonfirmMessage(getUserMes.UserMessage, getPeerId.PeerId);
                    }
                }

            }
        }    

 
    }
}