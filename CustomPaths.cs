using LabApi.Loader.Features.Paths;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AutoEvent_5KMode.API.Featrues.ServerTranslate;

namespace AutoEvent_5KMode.API.Featrues
{
    public class CustomPaths
    {
        public static string MusicPath;
        public static string TranslatePath;
        public static void RegiaterAllPath()
        {
            MusicPath = Path.Combine(PathManager.LabApi.ToString(), "AutoEvent_5KMode_Music");
            TranslatePath = Path.Combine(PathManager.LabApi.ToString(),"AutoEvent_5KMode_Translaties");
            if(!Directory.Exists(MusicPath))
            {
                Directory.CreateDirectory(MusicPath);
            }
            if(!Directory.Exists(TranslatePath))
            {
                Directory.CreateDirectory(TranslatePath);
            }
        }
        public static string GetTranslateFile(ServerLanguage serverLanguage, TranslateType translateType)
        {
            string filepath = Path.Combine(TranslatePath,serverLanguage.ToString()+"."+translateType.ToString()+".json");
            if(!File.Exists(filepath))
            {
                File.WriteAllText(filepath, "[]");
            }
            return filepath;
        }
    }
}
