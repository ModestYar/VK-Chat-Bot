using Mohostani;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.RequestParams;
using VkNet.Model;

namespace moxbot
{
    class SendingManager
    {
        public static void MessageToChat (string reply, long? peerId)
        {
            var message = new MessagesSendParams
            {
                Message = reply,
                PeerId = peerId,
                RandomId = 0,
                Intent = Intent.Default
            };
            Program.api.Messages.SendAsync(message);
        }
        public static void MessageToChat(uint? stickerId, long? peerId)
        {
            var message = new MessagesSendParams
            {
                StickerId = stickerId,
                PeerId = peerId,
                RandomId = 0,
                Intent = Intent.Default
            };
            Program.api.Messages.SendAsync(message);
        }

        public static void EditMessageIntoChat(string reply, long? peerId, long? chatMessageId)
        {
            Program.api.Messages.Edit(new MessageEditParams
            {
                Message = reply,
                ConversationMessageId = chatMessageId + 1,
                PeerId = (long)peerId,
            });
        }



    }
}
