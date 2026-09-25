using AutoEvent_5KMode.API.Featrues.AudioManager;
using LabApi.Features.Wrappers;
using MEC;
using NetworkManagerUtils.Dummies;
using ProjectMER.Events.Handlers;
using ProjectMER.Features;
using ProjectMER.Features.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using BaseLight = AdminToys.LightSourceToy;
using BaseWaypoint = AdminToys.WaypointToy;

namespace AutoEvent_5KMode.API.Featrues.LoadAniations
{
    public class SerpAniLoad
    {
        public static void Start(List<Player> players)
        {
            SchematicObject schematicObject;
            ReferenceHub referenceHub = DummyUtils.SpawnDummy("蛇之手登陆");
            DummyMusic dummyMusic = DummyMusic.GetOrAdd(referenceHub);
            Dictionary<GameObject, Vector3> GameObjectOldPos = new Dictionary<GameObject, Vector3>(); 
            foreach(Player player in players)
            {
                player.SetRole(PlayerRoles.RoleTypeId.Tutorial, PlayerRoles.RoleChangeReason.RemoteAdmin);
            }
            Timing.CallDelayed(4, () =>
            {
                dummyMusic.Play(Path.Combine(CustomPaths.MusicPath, "SerpLoad.ogg"), true);
                schematicObject = ObjectSpawner.SpawnSchematic("SerpLibrary", new UnityEngine.Vector3(0,0,0));
                WaypointToy waypointToy = (WaypointToy)WaypointToy.Get(schematicObject.AdminToyBases.FirstOrDefault(x => x is BaseWaypoint));
                GameObject target = schematicObject.AttachedBlocks.FirstOrDefault(x => x.name == "SpawnTarget");
                GameObject SerpIcon = schematicObject.AttachedBlocks.FirstOrDefault(x => x.name == "SerpIcon");
                players.ForEach(x => x.GameObject.transform.LookAt(target.transform));
                players.ForEach(x => x.Position =  waypointToy.Position);
                foreach(GameObject gameObject in schematicObject.AttachedBlocks)
                {
                    if(gameObject.TryGetComponent<BaseLight>(out BaseLight light))
                    {
                        if(light.LightColor.r == 83)
                        {
                            GameObjectOldPos.Add(light.gameObject, light.transform.position);
                            light.Position.Set(999, 999, 999);
                        }

                    }
                }
                SerpIcon.transform.position.Set(999, 999, 999);
                Timing.CallDelayed(12, () =>
                {
                    foreach(var kv in GameObjectOldPos)
                    {
                        GameObject gb = kv.Key;
                        Vector3 pos = GameObjectOldPos[gb];
                        gb.transform.position = pos;
                    }
                    foreach(Player player in Player.List)
                    {
                        player.SetRole(PlayerRoles.RoleTypeId.Tutorial);
                    }
                });
            });
            Timing.CallDelayed(15, () =>
            {
                dummyMusic.Destroy();

            });
        }
    }
}
