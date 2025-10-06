using Exiled.API.Features.Pickups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoEvent_5KMode.Items
{
    public class GOCQS
    {
        public static Pickup GolalPickup;
        public static void Spawn(Vector3 pos)
        {
            Pickup pickup = Pickup.CreateAndSpawn(ItemType.SCP244a, pos);
            SpecialItemManager.AddSpecial(pickup, SpecialItems.GOCQS);
            SpecialItemManager.SchematicFollowItem(pickup, "GOCQS");
            GolalPickup = pickup;
        }
    }
}
