using AutoEvent_5KMode.API.Featrues;
using LabApi.Events.Arguments.ServerEvents;
using LabApi.Events.CustomHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoEvent_5KMode.API.EventHamdlers
{
    public class MainEventHandler:CustomEventsHandler
    {
        public override void OnServerRoundStarted()
        {
            CustomHandlersManager.RegisterEventsHandler(new PlayerEventHandler());
            new MyServerSpecifiSettings().Activate();
            base.OnServerRoundStarted();
        }
        public override void OnServerRoundEnded(RoundEndedEventArgs ev)
        {
            CustomHandlersManager.UnregisterEventsHandler(new PlayerEventHandler());
            new MyServerSpecifiSettings().Deactivate();
            base.OnServerRoundEnded(ev);
        }
    }
}
