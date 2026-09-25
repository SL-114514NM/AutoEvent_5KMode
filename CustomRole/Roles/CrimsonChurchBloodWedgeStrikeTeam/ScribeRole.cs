using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoEvent_5KMode.API.Featrues.CustomRole.Roles.CrimsonChurchBloodWedgeStrikeTeam
{
    public class ScribeRole : CustomRole
    {
        public override string Name { get; set; } = "深红教会血楔打击组-铭文师";
        public override int Id { get; set; } = 3;
        public override RoleTypeId RoleType { get; set; } = RoleTypeId.Tutorial;
        public override List<ItemType> ItemTypes { get; set; } = new List<ItemType>()
        {
            ItemType.SCP1509,
            ItemType.GunCOM15,
            ItemType.Coin,
        };
        public override float MaxHealthy { get; set; } = 100;
        public override CustomSpawnPosition SpawnPosition { get; set; } = new CustomSpawnPosition(LabApi.Features.Enums.DoorName.Hcz096);
        public override string Description { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
