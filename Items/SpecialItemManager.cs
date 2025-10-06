using Exiled.API.Features.Items;
using Exiled.API.Features.Pickups;
using MEC;
using ProjectMER.Features;
using ProjectMER.Features.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoEvent_5KMode.Items
{
    public class SpecialItemManager
    {
        public static Dictionary<ushort, SpecialItems> SpecialItem=new Dictionary<ushort, SpecialItems>();
        public static Dictionary<ushort, SchematicObject> FollowSchematic = new Dictionary<ushort, SchematicObject>();
        public static bool AddSpecial(Pickup pickup, SpecialItems specialItem)
        {
            if (pickup==null) return false;
            if (SpecialItem.ContainsKey(pickup.Serial))
            {
                SpecialItem[pickup.Serial] = specialItem;
                return true;
            }
            SpecialItem.Add(pickup.Serial, specialItem);
            return true;
        }
        public static bool AddSpecial(Item pickup, SpecialItems specialItem)
        {
            if (pickup == null) return false;
            if (SpecialItem.ContainsKey(pickup.Serial))
            {
                SpecialItem[pickup.Serial] = specialItem;
                return true;
            }
            SpecialItem.Add(pickup.Serial, specialItem);
            return true;
        }
        public static bool IsSpecial(Pickup pickup, SpecialItems specialI)
        {
            if (!SpecialItem.TryGetValue(pickup.Serial, out var special))
                return false;
            if (special != specialI)
                return false;
            return true;
        }
        public static bool IsSpecial(Item pickup, SpecialItems specialI)
        {
            if (!SpecialItem.TryGetValue(pickup.Serial, out var special))
                return false;
            if (special != specialI)
                return false;
            return true;
        }
        public static bool RemoveSpecial(Pickup pickup)
        {
            return SpecialItem.Remove(pickup.Serial);
        }
        public static bool RemoveSpecial(Item pickup)
        {
            return SpecialItem.Remove(pickup.Serial);
        }
        public static bool SchematicFollowItem(Pickup pickup, string name)
        {
            SchematicObject schematicObject = ObjectSpawner.SpawnSchematic(
                name,
                pickup.Position,
                pickup.Rotation
                );
            if (schematicObject == null) return false;
            if (FollowSchematic.ContainsKey(pickup.Serial)) 
            { 
                FollowSchematic[pickup.Serial] = schematicObject;
                
                return true; 
            }
            FollowSchematic.Add(pickup.Serial, schematicObject);
            Timing.RunCoroutine(MECFollow(pickup, schematicObject));
            return true;
        }
        public static bool RemoveFollow(Pickup pickup)
        {
            return FollowSchematic.Remove(pickup.Serial);
        }
        public static IEnumerator<float> MECFollow(Pickup pickup, SchematicObject schematicObject)
        {
            while(true)
            {
                if (!FollowSchematic.ContainsKey(pickup.Serial))
                {
                    yield break;
                }
                if (pickup.PreviousOwner != null)
                {
                    schematicObject.Position = new UnityEngine.Vector3(0, 0, 0);
                }
                else
                {
                    schematicObject.Position = pickup.Position;
                    schematicObject.Rotation = pickup.Rotation;
                }
                yield return Timing.WaitForSeconds(1f);
            }
        }
    }
    public enum SpecialItems
    {
        /// <summary>
        /// GOC-奇术核弹
        /// </summary>
        GOCQS,
    }
}
