using LabApi.Features.Wrappers;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoEvent_5KMode.API.Featrues.CustomRole.Roles.CrimsonChurchBloodWedgeStrikeTeam
{
    public class ZealotRole : CustomRole
    {
        public override string Name { get; set; } = "深红教会血楔打击组-狂信者";
        public override int Id { get; set; } = 4;
        public override RoleTypeId RoleType { get; set; } = RoleTypeId.Tutorial;
        public override List<ItemType> ItemTypes { get; set; } = new List<ItemType>()
        {
            ItemType.GunCOM18,
        };
        public override float MaxHealthy { get; set; } = 80;
        public override CustomSpawnPosition SpawnPosition { get; set; } = new CustomSpawnPosition(RoleTypeId.Scp096);
        public override string Description { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void Spawn(Player player)
        {
            player.GiveCandy(InventorySystem.Items.Usables.Scp330.CandyKindID.Pink, InventorySystem.Items.ItemAddReason.AdminCommand);
            base.Spawn(player);
        }
    }
}
