using LabApi.Features.Wrappers;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoEvent_5KMode.API.Featrues
{
    public class AbilityManager
    {
        public static List<AbilityInfo> AbilityInfos = new List<AbilityInfo>();
        public static Dictionary<int, List<CustomAbility>> AbilitiesBySettingId = new Dictionary<int, List<CustomAbility>>();
        private static CoroutineHandle _playerabilitycoes;
        public static void RegisterAll()
        {
            if(_playerabilitycoes.IsRunning)
            {
                Timing.KillCoroutines(_playerabilitycoes);
            }
            _playerabilitycoes = Timing.RunCoroutine(HandlePlayerAbiilityCDAndOther());
        }
        public static void AddAbilityForPlayer(ReferenceHub hub, AbilityType abilityType,float cd =90)
        {
            List<AbilityInfo> infos = AbilityInfos.Where(x => x.Owner == hub).ToList();
            if(infos.Any(x=>x.AbilityType == abilityType))
            {
                return;
            }
            AbilityInfos.Add(new AbilityInfo(hub, abilityType, cd));
        }
        public static AbilityInfo GetAbilityInfo(ReferenceHub hub, AbilityType abilityType)
        {
            return AbilityInfos.FirstOrDefault(x=> x.Owner==hub&&x.AbilityType==abilityType);
        }
        public static void ChangeCooldown(ReferenceHub hub, AbilityType abilityType, float newcd)
        {
            AbilityInfos.FirstOrDefault(x=>x.Owner == hub&&x.AbilityType==abilityType).CoolDowns=newcd;
        }
        public static bool CanUseAbility(ReferenceHub hub, AbilityType abilityType)
        {
            List<AbilityInfo> infos = AbilityInfos.Where(x => x.Owner == hub).ToList();
            AbilityInfo ability = infos.FirstOrDefault(x=>x.AbilityType==abilityType);
            return ability.CoolDowns == 0;
        }
        private static IEnumerator<float> HandlePlayerAbiilityCDAndOther()
        {
            yield return Timing.WaitForSeconds(1);
            while(Round.IsRoundStarted)
            {
                foreach(var ability in AbilityInfos)
                {
                    if(ability.CoolDowns!=0)
                    {
                        ability.CoolDowns--;
                    }
                }
                yield return Timing.WaitForSeconds(1);
            }
            yield return Timing.WaitForSeconds(1);
        }
        public static void Clear()
        {
            AbilityInfos.Clear();
            Timing.KillCoroutines(_playerabilitycoes);
        }
        public class AbilityInfo
        {
            public AbilityInfo(ReferenceHub owner, AbilityType abilityType, float coolDowns)
            {
                Owner = owner;
                AbilityType = abilityType;
                CoolDowns = coolDowns;
            }

            public ReferenceHub Owner { get; set; }
            public AbilityType AbilityType { get; set; }
            public float CoolDowns { get; set; }
        }
        public enum AbilityType
        {
            Ability1=0,
            Ability2=1,
            Ability3=2,
        }
    }
}
