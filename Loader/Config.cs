using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoEvent_5KMode.Loader
{
    public class Config
    {
        public bool IsEnabled { get; set; } = true;
        [Description("今日凯撒密码")]
        public int CaesarKey { get; set; } = 0;
        [Description("Plugin Language, Can Use zh-CN or en-us")]
        public string PluginLanguage { get; set; } = "zh-CN";
    }
}
