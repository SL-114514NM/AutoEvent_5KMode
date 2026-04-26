using AutoEvent_5KMode.API;
using LabApi.Loader.Features.Paths;
using LabApi.Loader.Features.Plugins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoEvent_5KMode.Main
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "SL-5k模式";

        public override string Description => "5K模式";

        public override string Author => "灰(HUI)";

        public override Version Version => new Version(1,0);

        public override Version RequiredApiVersion => new Version(LabApi.Features.LabApiProperties.CompiledVersion);
        public static Plugin Instance;
        public string kkPath;
        public override void Enable()
        {
            Instance = this;
            this.kkPath = Path.Combine(PathManager.LabApi.ToString(), "5KMusics");
            if(!Directory.Exists(kkPath))
            {
                Directory.CreateDirectory(kkPath);
            }
            new NBServerSpifiecSettings().Activate();
            LabApi.Events.CustomHandlers.CustomHandlersManager.RegisterEventsHandler(new MyEventHandler());
        }
        public override void Disable()
        {
            Instance = null;
            new NBServerSpifiecSettings().Deactivate();
            LabApi.Events.CustomHandlers.CustomHandlersManager.UnregisterEventsHandler(new MyEventHandler());
        }
    }
}
