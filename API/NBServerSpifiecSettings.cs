using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserSettings.ServerSpecific;

namespace AutoEvent_5KMode.API
{
    public class NBServerSpifiecSettings : UserSettings.ServerSpecific.Examples.SSExampleImplementationBase
    {
        public override string Name => "5K-ServerSpecifiSettings";

        public override void Activate()
        {
            UserSettings.ServerSpecific.ServerSpecificSettingsSync.DefinedSettings = new UserSettings.ServerSpecific.ServerSpecificSettingBase[]
            {
                new UserSettings.ServerSpecific.SSGroupHeader(Name+"-5K服务器设置"),
                new SSKeybindSetting(1,"技能一", UnityEngine.KeyCode.Alpha0),
            };
        }

        public override void Deactivate()
        {
            
        }
    }
}
