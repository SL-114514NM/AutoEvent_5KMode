using AdminToys;
using AutoEvent_5KMode.API.Featrues.AudioManager;
using Mirror;
using NetworkManagerUtils.Dummies;
using PlayerRoles.FirstPersonControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoEvent_5KMode.API.Featrues.MoreProjectObjects
{
    public class MPortjectDummyPlayer
    {
        public MPortjectDummyPlayer() { }
        public MPortjectDummyPlayer(ReferenceHub referenceHub)
        {
            this.dummy = referenceHub;
            if (referenceHub.roleManager.CurrentRole is IFpcRole fpcRole)
            {
                var mouseLook = fpcRole.FpcModule.MouseLook;
                this.Rotation = new Vector2(mouseLook.CurrentVertical, mouseLook.CurrentHorizontal);
            }
            this.Position = referenceHub.GetPosition();
        }
        public Vector3 Position { get; set; }
        public Vector2 Rotation { get; set; }
        public ReferenceHub dummy;
        public static MPortjectDummyPlayer CreateDummyPlayer(ReferenceHub referenceHub)
        {
            return new MPortjectDummyPlayer(referenceHub);
        }
        public static MPortjectDummyPlayer CreateDummyPlayer(string name, Vector3 pos,Vector2 vector2)
        {
            ReferenceHub hub = DummyUtils.SpawnDummy(name);
            hub.TryOverridePosition(pos);
            hub.TryOverrideRotation(vector2);
            return new MPortjectDummyPlayer(hub);
        }
        public void UpdataPosAndRot(Vector3 newpos, Vector2 newrot)
        {
            dummy.TryOverridePosition(newpos);
            dummy.TryOverrideRotation(newrot);
            FpcMouseLook fpcMouseLook = (FpcMouseLook)(dummy.roleManager.CurrentRole as IFpcRole);
            this.Rotation = new Vector2(fpcMouseLook.CurrentVertical, fpcMouseLook.CurrentHorizontal);
            this.Position = dummy.GetPosition();
        }
        public void FllowWayposint(WaypointToy waypointToy)
        {
            if (waypointToy == null) return;
            dummy.transform.LookAt(waypointToy.transform.forward);
            dummy.TryOverridePosition(waypointToy.Position);
            dummy.transform.SetParent(waypointToy.transform);
        }
        public DummyMusic GetDummyMusic()
        {
            return DummyMusic.GetOrAdd(dummy);
        }
        public void DestroyDummy()
        {
            NetworkServer.Destroy(dummy.gameObject);
        }
    }
}
