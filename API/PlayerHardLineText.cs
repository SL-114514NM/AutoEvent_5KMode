using LabApi.Features.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoEvent_5KMode.API
{
    public class PlayerHardLineText
    {
        public static Dictionary<int, TextToy> ToysByPlayer = new Dictionary<int, TextToy>();
        public static void AddNew(Player player, string content)
        {
            TextToy textToy = TextToy.Create();
            textToy.Position = player.Position + Vector3.up;
            textToy.TextFormat = content;
            if(ToysByPlayer.ContainsKey(player.PlayerId))
            {
                ToysByPlayer[player.PlayerId] = textToy;
            }
            else
            {
                ToysByPlayer.Add(player.PlayerId, textToy);
            }
            textToy.Spawn();
        }
    }
}
