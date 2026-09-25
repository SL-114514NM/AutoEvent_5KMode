using AdminToys;
using LabApi.Features.Wrappers;
using NetworkManagerUtils.Dummies;
using PlayerRoles;
using ProjectMER.Features.Extensions;
using ProjectMER.Features.Serializable;
using ProjectMER.Features.Serializable.Schematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoEvent_5KMode.API.Featrues.MoreProjectObjects
{
    public class SerializableDummyPlayer: SerializableObject
    {
        public string NickName { get; set; }
        public RoleTypeId RoleTypeId { get; set; }
        public ReferenceHub Dummy;
        public MPortjectDummyPlayer MPortjectDummyPlayer;
        public override GameObject SpawnOrUpdateObject(Room room = null, GameObject instance = null)
        {
            if(Dummy == null)
            {
                Dummy = DummyUtils.SpawnDummy(NickName);
            }
            Vector3 absolutePosition = room.GetAbsolutePosition(this.Position);
            Quaternion absoluteRotation = room.GetAbsoluteRotation(this.Rotation);
            MPortjectDummyPlayer = MPortjectDummyPlayer.CreateDummyPlayer(Dummy);
            MPortjectDummyPlayer.UpdataPosAndRot(absolutePosition, new Vector2(absoluteRotation.x, absoluteRotation.y));
            GameObject gameObject = Dummy.gameObject;
            if (instance == null)
            {
                DummyUtils.SpawnDummy(NickName);
            }
            return gameObject;
        }
    }
}
