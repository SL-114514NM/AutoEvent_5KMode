using AutoEvent_5KMode.API.Featrues.AudioManager;
using AutoEvent_5KMode.API.Featrues.MoreProjectObjects;
using LabApi.Features.Wrappers;
using MEC;
using NetworkManagerUtils.Dummies;
using PlayerRoles.FirstPersonControl;
using ProjectMER.Features;
using ProjectMER.Features.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoEvent_5KMode.API.Featrues.SpecifiEnding
{
    /// <summary>
    /// GOC启动MGR核弹结局
    /// </summary>
    public class GOCMGREnding
    {
        public static MPortjectDummyPlayer DummyPlayer;
        public static SchematicObject MGRObject;
        public static List<GameObject> TelportGameObjects = new List<GameObject>();
        public static void Start(Player player)
        {
            if (player == null) return;
            MGRObject = ObjectSpawner.SpawnSchematic("MGRStart", player.Position);
            MyAPI.SendPlayerTimeTypeHint(player, 600, HintServiceMeow.Core.Enum.HintAlignment.Center, new List<string> { "<color=blue>MGR风暴核弹已完成Site-02站点的安置</color>","现任务变更:","守护MGR核弹，撑到该设施被奇术打击销毁" }, 3);
            DummyMusic dummyMusic = DummyMusic.GetOrAdd(DummyUtils.SpawnDummy("GOC-MGR-Starting"));
            dummyMusic.Play(Path.Combine(CustomPaths.MusicPath, "GOCMGR.ogg"));
            Timing.CallDelayed(200, () =>
            {
                if(MGRObject == null) return;
                Round.IsLocked = true;
                List<ReferenceHub> allHubs = ReferenceHub.AllHubs.ToList();
                allHubs.Remove(DummyPlayer.dummy);
                foreach (ReferenceHub hub in allHubs)
                {
                    Player Target = Player.Get(hub);
                    Target.SetRole(PlayerRoles.RoleTypeId.Spectator);
                    Target.SendHint("<mark=blue>史诗之证, 在此呈现</mark>",3);
                    MyAPI.SendPlayerTimeTypeHint(Target, 600, HintServiceMeow.Core.Enum.HintAlignment.Center, new List<string> {"伙计，任务又完成了，但这是最后一次","你还记得上次的聚会吗?","那是我们最后的欢乐了","伙计，我也给你准备了一个房间-----" }, 5);
                    
                }
                Announcer.Message("", "达成结局<mark=blue>史诗之证</mark>, GOC启动了MGR摧毁了设施");
                Announcer.Message("", "达成结局<mark=blue>史诗之证</mark>, GOC启动了MGR摧毁了设施");
                Announcer.Message("", "达成结局<mark=blue>史诗之证</mark>, GOC启动了MGR摧毁了设施");
                Announcer.Message("", "达成结局<mark=blue>史诗之证</mark>, GOC启动了MGR摧毁了设施");
                Timing.RunCoroutine(CheckAndTel());
            });
        }
        public static IEnumerator<float> CheckAndTel()
        {
            while(true)
            {
                if(NearTelToSuf(DummyPlayer.dummy))
                {
                    SlowMoveDummy(DummyPlayer.dummy, 0.5f, false);
                }
                else
                {
                    if(Vector3.Distance(DummyPlayer.dummy.GetPosition(), MGRObject.transform.position) <=9)
                    {
                        yield break;
                    }
                    DummyPlayer.dummy.PlayerCameraReference.LookAt(MGRObject.transform);
                    SlowMoveDummy(DummyPlayer.dummy, 0.5f, true);
                }
                if (DummyPlayer == null) yield break;
                if (MGRObject == null) yield break;
                yield return Timing.WaitForSeconds(1f);
            }
        }
        public static bool NearTelToSuf(ReferenceHub target)
        {
            if (TelportGameObjects.Any(x => x.name == "teltosuf"))
            {
                GameObject gameObject = TelportGameObjects.FirstOrDefault(x => x.name == "teltosuf");
                if(Vector3.Distance(target.GetPosition(), gameObject.transform.position) < 2f)
                {
                    target.TryOverridePosition(Door.Get(LabApi.Features.Enums.DoorName.SurfaceGate).Position + new Vector3(0, 1, 0));
                    return true;
                }
                return false;
            }
            return false;
        }
        public static void SlowMoveDummy(ReferenceHub hub, float moveSpeed, bool movingForward)
        {
            Vector3 directionToCamera = (hub.PlayerCameraReference.transform.position - hub.GetPosition()).normalized;
            Vector3 moveDirection = movingForward ? directionToCamera : -directionToCamera;
            Vector3 deltpos = moveDirection * moveSpeed * Time.deltaTime;
            hub.TryOverridePosition(hub.GetPosition() + deltpos);
        }
    }
}
