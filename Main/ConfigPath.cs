using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoEvent_5KMode.Main
{
    public class ConfigPath
    {
        public static string RootPath = Plugin.Instance.Config.RootPath;
        public static string MusicPath = Path.Combine(RootPath, "Audio");
        public static void Int()
        {
            RootPath = Plugin.Instance.Config.RootPath;
            MusicPath = Path.Combine(RootPath, "Audio");
            if (!Directory.Exists(RootPath))
            {
                Directory.CreateDirectory(RootPath);
            }
            if (!Directory.Exists(MusicPath))
            {
                Directory.CreateDirectory(MusicPath);
            }
            Log.Debug($"SCP:5K主文件夹已生成");
            Log.Debug($"主目录[{RootPath}]");
            Log.Debug($"音频目录[{MusicPath}]");
        }
    }
}
