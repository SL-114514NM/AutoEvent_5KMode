using AutoEvent_5KMode.API.Featrues;
using LabApi.Features.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AutoEvent_5KMode.API.Featrues.AbilityManager;

namespace AutoEvent_5KMode.API.Featrues
{
    public abstract class CustomAbility
    {
        public static List<CustomAbility> CustomAbilities = new List<CustomAbility>();
        public static CustomAbility GetCustomAbility(Player Owner, AbilityType abilityType)
        {
            return CustomAbilities.FirstOrDefault(x=> x.Owner ==Owner&&x.AbilityType ==abilityType);
        }
        public static CustomAbility GetCustomAbility(int Id)
        {
            return CustomAbilities.FirstOrDefault(x=> x.Id == Id);
        }
        public abstract int Id { get; set; }
        public abstract string Name { get; set; }
        public abstract string Description { get; set; }
        public abstract AbilityType AbilityType { get; set; }
        public Player Owner;
        public virtual float CoolDown
        {
            get
            {
                return AbilityManager.GetAbilityInfo(Owner.ReferenceHub, AbilityType).CoolDowns;
            }
            set
            {
                AbilityManager.ChangeCooldown(Owner.ReferenceHub, AbilityType, value);
            }
        }
        public bool IsActive
        {
            get
            {
                return !AbilityManager.CanUseAbility(Owner.ReferenceHub, AbilityType);
            }
        }
        public bool CanUse => !IsActive;
        public virtual void AddToPlayer(Player player)
        {
            this.Owner = player;
            AbilityManager.AddAbilityForPlayer(player.ReferenceHub, AbilityType, CoolDown);
            CustomAbilities.Add(this);
            switch(AbilityType)
            {
                case AbilityType.Ability1:
                    if(AbilityManager.AbilitiesBySettingId.ContainsKey(114532))
                    {
                        AbilityManager.AbilitiesBySettingId[114532].Add(this);
                    }
                    else
                    {
                        AbilityManager.AbilitiesBySettingId.Add(114532, new List<CustomAbility>() { this});
                    }
                    return;
                case AbilityType.Ability2:
                    if (AbilityManager.AbilitiesBySettingId.ContainsKey(114533))
                    {
                        AbilityManager.AbilitiesBySettingId[114532].Add(this);
                    }
                    else
                    {
                        AbilityManager.AbilitiesBySettingId.Add(114532, new List<CustomAbility>() { this });
                    }
                    return;
                case AbilityType.Ability3:
                    if (AbilityManager.AbilitiesBySettingId.ContainsKey(114534))
                    {
                        AbilityManager.AbilitiesBySettingId[114532].Add(this);
                    }
                    else
                    {
                        AbilityManager.AbilitiesBySettingId.Add(114532, new List<CustomAbility>() { this });
                    }
                    return;
            }
        }
        public virtual void HandleAbility()
        {
            //技能触发时写的代码
            if(CanUse)
            {
                AbilityManager.AbilityInfos.FirstOrDefault(x => x.Owner == Owner.ReferenceHub && x.AbilityType == AbilityType).CoolDowns = this.CoolDown;
            }
        }
    }
}
