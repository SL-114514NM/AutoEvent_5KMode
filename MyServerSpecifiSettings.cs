using HarmonyLib;
using LabApi.Features.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserSettings;
using UserSettings.ServerSpecific;
using UserSettings.ServerSpecific.Examples;

namespace AutoEvent_5KMode.API.Featrues
{
    public class MyServerSpecifiSettings : SSExampleImplementationBase
    {
        public override string Name => "Auto_5K_ServerSpecifiSettings";

        public override void Activate()
        {
            ServerSpecificSettingsSync.DefinedSettings.AddItem(new SSKeybindSetting(114532, "5K插件技能一"));
            ServerSpecificSettingsSync.DefinedSettings.AddItem(new SSKeybindSetting(114533, "5K插件技能二"));
            ServerSpecificSettingsSync.DefinedSettings.AddItem(new SSKeybindSetting(114534, "5K插件技能三"));
            ServerSpecificSettingsSync.SendToAll();
            ServerSpecificSettingsSync.ServerOnSettingValueReceived += OnSSSS;
        }

        public override void Deactivate()
        {
            ServerSpecificSettingsSync.ServerOnSettingValueReceived -= OnSSSS;
        }
        public void OnSSSS(ReferenceHub hub, ServerSpecificSettingBase ssb)
        {
            if (hub == null) return;
            if (ssb == null) return;
            if(ssb.SettingId!=114532||ssb.SettingId!=114533||ssb.SettingId!=114534) return;
            List<CustomAbility> customAbilities = AbilityManager.AbilitiesBySettingId[ssb.SettingId];
            if (customAbilities.Count==0) return;
            CustomAbility TargetAbility = customAbilities.FirstOrDefault(x => x.Owner.ReferenceHub == hub);
            if(TargetAbility == null) return;
            TargetAbility.HandleAbility();
        }
    }
}
