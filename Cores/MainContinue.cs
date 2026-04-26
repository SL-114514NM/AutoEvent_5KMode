using AutoEvent_5KMode.API;
using LabApi.Features.Wrappers;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoEvent_5KMode.Cores
{
    public class MainContinue
    {
        public static IEnumerable<float> NBContinue()
        {
            yield return Timing.WaitForSeconds(1);
            while (Round.IsRoundStarted)
            {
                foreach (Player player in Player.List)
                {
                    if(PlayerHardLineText.ToysByPlayer.ContainsKey(player.PlayerId))
                    {
                        TextToy textToy = PlayerHardLineText.ToysByPlayer[player.PlayerId];
                        textToy.Position = player.Position + Vector3.up;
                    }
                }
                yield return Timing.WaitForSeconds(1);
            }
        }
    }
}
