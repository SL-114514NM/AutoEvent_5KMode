using Exiled.API.Features;
using Exiled.API.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoEvent_5KMode.Main
{
    public class Config:IConfig
    {
        bool IConfig.IsEnabled { get; set; }
        bool IConfig.Debug { get; set; }
        [Description("SCP:5K主目录")]
        public string RootPath { get; set; } = Path.Combine(Paths.Exiled, "SCP:5K");
    }
}
