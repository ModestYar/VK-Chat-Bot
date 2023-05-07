using VkNet;
using VkNet.Model;
using VkNet.Model.RequestParams;
using VkNet.Model.GroupUpdate;
using VkNet.Enums.SafetyEnums;
using moxbot;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Mohostani
{
    class Program
    {        
        static string key = ConfigurationManager.AppSettings["VKToken"];
        static int groupId = Convert.ToInt32(ConfigurationManager.AppSettings["GroupID"]);
        static SqlConnection sqlConnection;
        public static VkApi api = new VkApi();
        public string? UserMessage { get; set; }
        public long? PeerId { get; set; }
        public long? ChatMessageId { get; set; }
        

        
        public static void Main(string[] args)
        {
            Auth();
            SqlConnect();
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


                var ConnectionData = api.Groups.GetLongPollServer(ulong.Parse(groupId.ToString()));

                var poll = api.Groups.GetBotsLongPollHistory(new BotsLongPollHistoryParams()
                {
                    Server = ConnectionData.Server,

                    Ts = ConnectionData.Ts,

                    Key = ConnectionData.Key,

                    Wait = 25

                });

                //проверяем появилось ли новое событие

                if (poll?.Updates == null)
                {
                    LongPoll();
                }
                //»копаемся» в новом событие

                foreach (var a in poll.Updates)
                {
                    if (a.Type.Value == GroupUpdateType.MessageNew)
                    {

                      getUserMes.UserMessage = ((MessageNew)a.Instance).Message.Text.ToLower();

                        getPeerId.PeerId = ((MessageNew)a.Instance).Message.PeerId;

                        getChatMesId.ChatMessageId = ((VkNet.Model.GroupUpdate.MessageNew)a.Instance).Message.ConversationMessageId;

                        Controller.MessageToChat (getUserMes.UserMessage, getPeerId.PeerId, sqlConnection);

                        Controller.StickerToChat(getUserMes.UserMessage, getPeerId.PeerId);

                        Controller.Roullete(getUserMes.UserMessage, getPeerId.PeerId);

                        Controller.TextAniimation(getUserMes.UserMessage, getPeerId.PeerId, getChatMesId.ChatMessageId);

                        Controller.СonfirmMessage(getUserMes.UserMessage, getPeerId.PeerId, sqlConnection);

                        Controller.SeparateMessage(getUserMes.UserMessage, getPeerId.PeerId);

                        Controller.RandomMessage(getUserMes.UserMessage, getPeerId.PeerId);
                    }
                }
            LongPoll();
        }

        public static void SqlConnect()
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MoxDB"].ConnectionString);

            sqlConnection.Open();

            if (sqlConnection.State == ConnectionState.Open)
                Console.WriteLine("БАЗА ПОДРУБЛЕНА");
            else
                Console.WriteLine("НЕ РОБЯТ БАЗЫ");
        }

        
    }
}