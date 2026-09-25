using LabApi.Features.Wrappers;
using Mirror;
using PlayerRoles.FirstPersonControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoEvent_5KMode.API.Featrues.GameObjectScripts
{
    public class ReboundGameObject:MonoBehaviour
    {
        public Player Owner;
        public float pushcount=0;
        public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<ReferenceHub>(out ReferenceHub hub))
            {
                if (hub == null) return;
                Vector3 camForward = hub.PlayerCameraReference.transform.forward;
                camForward.y = 0f;
                if (camForward.sqrMagnitude < 0.0001f)
                {
                    camForward = hub.PlayerCameraReference.transform.up;
                    camForward.y = 0f;
                }
                camForward.Normalize();
                Vector3 newpos = hub.GetPosition() + camForward * 2;
                if(Physics.Raycast(hub.GetPosition(), hub.PlayerCameraReference.forward, out RaycastHit hit,1))
                {
                    if (hit.collider.gameObject != gameObject) return;
                    if (hub.roleManager.CurrentRole.Team == Owner.Team) return;
                    pushcount++;
                    hub.TryOverridePosition(newpos);
                }
            }
        }
        public void OnCollisionExit(Collision collision)
        {
            if(pushcount >= 7)
            {
                NetworkServer.Destroy(gameObject);
            }
        }
    }
}
