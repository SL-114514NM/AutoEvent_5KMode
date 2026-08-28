using LabApi.Features.Enums;
using LabApi.Features.Wrappers;
using MapGeneration;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoEvent_5KMode.API.Featrues
{
    public class CustomSpawnPosition
    {
        public CustomSpawnPosition(Vector3 pos) { this.Position = pos; }
        public CustomSpawnPosition(RoomName roomName) { this.Position = Room.Get(roomName).FirstOrDefault().Position + Vector3.up; }
        public CustomSpawnPosition(RoleTypeId role) { this.Position = MyAPI.GetRolePos(role); }
        public CustomSpawnPosition(DoorName doorName) { this.Position = MyAPI.GetDoorPos(doorName); }
        public Vector3 Position;
    }
}
