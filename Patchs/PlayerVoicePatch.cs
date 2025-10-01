using Exiled.API.Features;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoiceChat;
using VoiceChat.Networking;

namespace AutoEvent_5KMode.Patchs
{
    public class PlayerVoicePatch
    {
        [HarmonyPatch(typeof(VoiceTransceiver), "ServerReceiveMessage")]
        public class NBPatch
        {
            private void Prefix(ReferenceHub referenceHub, VoiceMessage message)
            {
                if (referenceHub != null&&PlayerChatChannel.ContainsKey(Player.Get(referenceHub))&& Player.Get(referenceHub).IsAlive)
                {
                    message.Channel = PlayerChatChannel[Player.Get(referenceHub)];
                    referenceHub.connectionToClient.Send<VoiceMessage>(message);
                }
            }
        }
        public static Dictionary<Player, VoiceChatChannel> PlayerChatChannel = new Dictionary<Player, VoiceChatChannel>();
    }
}
