using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoEvent_5KMode.API.Featrues.CustomRole.Roles.CrimsonChurchBloodWedgeStrikeTeam
{
    public class BloodBound : CustomRole
    {
        public override string Name { get; set; } = "深红教会血楔打击组-血契者";
        public override int Id { get; set; } = 2;
        public override RoleTypeId RoleType { get; set; } = RoleTypeId.Tutorial;
        public override List<ItemType> ItemTypes { get; set; } = new List<ItemType>()
        {
            ItemType.ArmorHeavy,
            ItemType.GunE11SR,
            ItemType.Coin,
            ItemType.GunLogicer,
            ItemType.SCP1509,
        };
        public override float MaxHealthy { get; set; } = 250;
        public override CustomSpawnPosition SpawnPosition { get; set; } = new CustomSpawnPosition(LabApi.Features.Enums.DoorName.Hcz096);
        public override string Description { get; set; } = "";
    }
}
