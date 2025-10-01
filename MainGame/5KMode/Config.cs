using AutoEvent.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoEvent_5KMode.MainGame._5KMode
{
    public class Config:EventConfig
    {
        public static float GOCMaxHealth = 250;
        public static List<ItemType> GOCItems = new List<ItemType>()
        { 
            ItemType.GunFRMG0,
            ItemType.KeycardO5,
            ItemType.Medkit,
            ItemType.Medkit,
            ItemType.Medkit,
            ItemType.SCP1344
        };
    }
}
