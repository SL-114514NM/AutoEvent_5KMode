using AutoEvent.API;
using AutoEvent.Interfaces;
using Exiled.API.Enums;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoEvent_5KMode.MainGame._5KMode
{
    public class Config:EventConfig
    {
        [Description("GOC阵营角色配置")]
        public List<Loadout> GOCLoadout { get; set; } = new List<Loadout>()
        {
            new Loadout()
            {
                Items = new List<ItemType>(){ ItemType.ArmorHeavy, ItemType.GunFRMG0, ItemType.Medkit, ItemType.KeycardMTFCaptain},
                Health = 250,
                Roles = new Dictionary<RoleTypeId, int>()
                {
                    { RoleTypeId.Tutorial, 100 }
                },
                Effects = new List<Exiled.API.Features.Effect>(){new Exiled.API.Features.Effect(EffectType.MovementBoost, 0, 60)}
            }
        };
        [Description("Nu22特遣队角色配置")]
        public List<Loadout> Nu22Loadout { get; set; } = new List<Loadout>()
        {
            new Loadout()
            {
                Items = new List<ItemType>(){ ItemType.ArmorHeavy, ItemType.GunFRMG0, ItemType.KeycardO5, ItemType.Medkit},
                Health = 250,
                Roles = new Dictionary<RoleTypeId, int>()
                {
                    { RoleTypeId.Tutorial, 50 },
                    { RoleTypeId.NtfPrivate, 50 }
                },
                Effects = new List<Exiled.API.Features.Effect>(){new Exiled.API.Features.Effect(EffectType.MovementBoost, 0, 90)}
            }
        };
        [Description("SCP1440角色配置")]
        public List<Loadout> SCP1440 = new List<Loadout>()
        {
            new Loadout()
            {
                Items = new List<ItemType>(){ ItemType.Coin, ItemType.KeycardFacilityManager},
                Health = 199,
                Roles = new Dictionary<RoleTypeId, int>(){ { RoleTypeId.Tutorial, 100 } }
            }
        };
    }
}
