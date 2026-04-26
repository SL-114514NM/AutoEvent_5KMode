using LabApi.Features.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoEvent_5KMode.API
{
    public class ItemManager
    {
        public static Dictionary<ushort, SpecifiItem> ItemByIds = new Dictionary<ushort, SpecifiItem>();
        public static void Add(ushort id, SpecifiItem item)
        {
            if (ItemByIds.ContainsKey(id))
            {
                ItemByIds[id] = item;
                return;
            }
            ItemByIds.Add(id, item);
        }
        public static void Add(Item item, SpecifiItem specifiItem)
        {
            Add(item.Serial, specifiItem);
        }
        public static void Add(Pickup pickup, SpecifiItem specifiItem)
        {
            Add(pickup.Serial, specifiItem);
        }
        public static bool CheckItem(ushort id)
        {
            if (ItemByIds.ContainsKey(id))
                return true;
            return false;
        }
        public static bool CheckItem(ushort id, SpecifiItem specifiItem)
        {
            if (CheckItem(id))
            {
                if (ItemByIds[id] == specifiItem)
                    return true;
            }
            return false;
        }
        public static bool CheckItem(Item item, SpecifiItem specifiItem)
        {
            return CheckItem(item.Serial, specifiItem);
        }
        public static bool CheckItem(Pickup pickup, SpecifiItem specifiItem)
        {
            return CheckItem(pickup.Serial, specifiItem);
        }
        public static void Remove(ushort id)
        {
            ItemByIds.Remove(id);
        }
        public static void Remove(Item item)
        {
            Remove(item.Serial);
        }
        public static void Remove(Pickup pickup)
        {
            Remove(pickup.Serial);
        }
        public enum SpecifiItem
        {
            现实锁定锚,
            现实扭曲锚,
            轨道炮,
            OmegaKeycard,
            学术问题认证卡,
            M110大狙,
            SCP008病毒试剂,
            炮台放置器,
        }
    }
}
