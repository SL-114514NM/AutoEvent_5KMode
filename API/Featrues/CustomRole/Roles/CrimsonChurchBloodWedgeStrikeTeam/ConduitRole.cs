using LabApi.Features.Wrappers;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoEvent_5KMode.API.Featrues.CustomRole.Roles.CrimsonChurchBloodWedgeStrikeTeam
{
    public class ConduitRole : CustomRole
    {
        public override string Name { get; set; } = "深红教会血楔打击组-引导者";
        public override int Id { get; set; } = 1;
        public override RoleTypeId RoleType { get; set; } = RoleTypeId.Tutorial;
        public override List<ItemType> ItemTypes { get; set; } = new List<ItemType>()
        {
            ItemType.ArmorHeavy,
            ItemType.Coin,
            ItemType.GunE11SR,
            ItemType.GunCOM15,
            ItemType.SCP1509,
        };
        public override float MaxHealthy { get; set; } = 150;
        public override CustomSpawnPosition SpawnPosition { get; set; } = new CustomSpawnPosition(LabApi.Features.Enums.DoorName.Hcz096);
        public override string Description { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void Spawn(Player player)
        {
            MyAPI.SendPlayerTimeTypeHint(player, 800, HintServiceMeow.Core.Enum.HintAlignment.Center, new List<string>() { "<color=red>你听见了吗？祂在呼唤我们……</color>", "<color=red>第七个新娘……必须出生。</color>" }, 1);
            base.Spawn(player);
        }
    }
}
