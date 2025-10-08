using Exiled.API.Features;
using HarmonyLib;
using Mirror;
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
            static void Prefix(NetworkConnection conn, VoiceMessage msg)
            {
                if (PlayerChatChannel.ContainsKey(Player.Get(conn)))
                {
                    msg.Channel = PlayerChatChannel[Player.Get(conn)];
                    conn.Send(msg);
                }
            }
        }
        public static Dictionary<Player, VoiceChatChannel> PlayerChatChannel = new Dictionary<Player, VoiceChatChannel>();
    }
}
