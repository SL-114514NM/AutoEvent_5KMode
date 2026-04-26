using AutoEvent_5KMode.API;
using LabApi.Features.Wrappers;
using NetworkManagerUtils.Dummies;
using PlayerRoles.FirstPersonControl;
using ProjectMER.Features;
using ProjectMER.Features.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoEvent_5KMode.Items
{
    public class AutoShootTai
    {
        public static Dictionary<SchematicObject, int> PaoTaisByOwner = new Dictionary<SchematicObject, int>();
        public static Dictionary<SchematicObject, ReferenceHub> DummyByPaoTai = new Dictionary<SchematicObject, ReferenceHub>();
        public static void Spawn(Player Owner, Vector3 pos)
        {
            SchematicObject schematic = ObjectSpawner.SpawnSchematic("PaoTai", pos);
            PaoTaisByOwner.Add(schematic, Owner.PlayerId);
            ReferenceHub Bot = DummyUtils.SpawnDummy("PaoTaiDummy");
            SAPI.VisibilityPlayers.Add(Player.Get(Bot));
            DummyByPaoTai.Add(schematic, Bot);
            Player.Get(Bot).Health = 500;
            Bot.TryOverridePosition(pos + Vector3.up * 2);
            Pickup pickup = Pickup.Create(ItemType.GunE11SR, Vector3.zero);
            pickup.Spawn();
            Player.Get(Bot).AddItem(pickup);
            Bot.inventory.ClientSelectItem(pickup.Serial);
        }
    }
}
