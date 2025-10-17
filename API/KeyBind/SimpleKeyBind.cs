using Exiled.API.Features;
using Exiled.API.Features.Core;
using Exiled.API.Features.Core.UserSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UserSettings.ServerSpecific;

namespace AutoEvent_5KMode.API.KeyBind
{
    public class SimpleKeyBind
    {
        public static Dictionary<ReferenceHub, KeybindSetting> PlayerKeybindSetting = new Dictionary<ReferenceHub, KeybindSetting>();
        public static SettingBase RegisterAbility(Player player)
        {
            KeybindSetting keybindSetting = new KeybindSetting(PlayerKeybindSetting.Keys.Count + 1,"特殊角色按键技能",UnityEngine.KeyCode.LeftAlt);
            keybindSetting.HintDescription = "自定义技能按键绑定";
            if (PlayerKeybindSetting.ContainsKey(player.ReferenceHub))
            {
                PlayerKeybindSetting[player.ReferenceHub] = keybindSetting;
            }
            PlayerKeybindSetting.Add(player.ReferenceHub, keybindSetting);
            SettingBase settingBase = keybindSetting as SettingBase;
            SettingBase.Register(player, new List<SettingBase> { settingBase });
            return settingBase;
        }
    }
}
