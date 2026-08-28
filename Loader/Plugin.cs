using AutoEvent_5KMode.API.EventHamdlers;
using AutoEvent_5KMode.API.Featrues;
using AutoEvent_5KMode.API.Featrues.CustomRole;
using HarmonyLib;
using LabApi.Loader.Features.Plugins;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoEvent_5KMode.Loader
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "SL-5K插件";

        public override string Description => "";

        public override string Author => "灰";

        public override Version RequiredApiVersion => new Version(LabApi.Features.LabApiProperties.CompiledVersion);
        public static Plugin Instance { get; private set; }
        public Harmony KHarmony { get; private set; }
        public override void Enable()
        {
            Instance = this;
            CustomRoleManager.CheckAndCreateInstance();
            CustomRoleManager.RegisterAll();
            CustomPaths.RegiaterAllPath();
            ServerTranslate.RegisterAllTranslate();
            LabApi.Events.CustomHandlers.CustomHandlersManager.RegisterEventsHandler(new MainEventHandler());
            KHarmony = new Harmony("com.plugin.kk");
            KHarmony.PatchAll();
        }
        public override void Disable()
        {
            Instance = null;
            CustomRoleManager.UnRegisterAll();
            Timing.KillCoroutines();
            LabApi.Events.CustomHandlers.CustomHandlersManager.UnregisterEventsHandler(new MainEventHandler());
            KHarmony.UnpatchAll();
        }
    }
}
