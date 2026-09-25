using LabApi.Features.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoEvent_5KMode.API.Featrues.CustomItem
{
    public abstract class CustomItem
    {
        public static List<CustomItem> CustomItems = new List<CustomItem>();
        public static CustomItem Get(uint Id)
        {
            if(!CustomItems.Any(x => x.ID==Id))
            {
                return null;
            }
            return CustomItems.FirstOrDefault(x=> x.ID==Id);
        }
        public static void RegisterItem(CustomItem customItem)
        {
            if(CustomItems.Any(x => x.ID == customItem.ID))
            {
                return;
            }
            CustomItems.Add(customItem);
            customItem.OnEnabled();
        }
        public static void UnRegister(CustomItem customItem)
        {
            if (!CustomItems.Any(x => x.ID == customItem.ID))
            {
                return;
            }
            CustomItems.Remove(customItem);
            customItem.OnDisabled();
        }
        public abstract uint ID { get; set; }
        public abstract string Name { get; set; }
        public abstract ItemType ItemType { get; set; }
        public abstract void OnEnabled();
        public abstract void OnDisabled();
        public abstract CustomSpawnPosition SpawnPosition { get; set; }
        public Dictionary<uint, ushort> CustomItemBools = new Dictionary<uint, ushort>();
        public Pickup PickupInstance;
        public Item ItemInstance;
        public virtual Vector3 Scale
        {
            get
            {
                if(PickupInstance!=null)
                {
                    return PickupInstance.GameObject.transform.localScale;
                }
                if (ItemInstance != null)
                {
                    return ItemInstance.GameObject.transform.localScale;
                }
                return Vector3.one*1.3f;
            }
            set
            {
                if(PickupInstance!=null)
                {
                    PickupInstance.GameObject.transform.localScale = value;
                }
                if(ItemInstance!=null)
                {
                    ItemInstance.GameObject.transform.localScale = value;
                }
            }
        }
        public bool IsItem(ushort itemid)
        {
            if (!CustomItemBools.ContainsKey(ID)) return false;
            if (CustomItemBools[ID] != itemid) return false;
            return true;
        }
        public void Spawn()
        {
            Pickup pickup = Pickup.Create(ItemType,SpawnPosition.Position);
            this.PickupInstance = pickup;
            CustomItemBools.Add(ID, pickup.Serial);
            if(Scale!=null)
            {
                pickup.GameObject.transform.localScale = Scale;
            }
            pickup.Spawn();
        }
        public void Give(Player player)
        {
            Item item = player.AddItem(ItemType);
            this.ItemInstance = item;
            CustomItemBools.Add(ID, item.Serial);
            if(Scale!=null)
            {
                item.GameObject.transform.localScale = Scale;
            }
        }
    }
}
