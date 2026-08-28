using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoEvent_5KMode.API.Featrues.CustomRole.Roles.CrimsonChurchBloodWedgeStrikeTeam
{
    public class PaganRole : CustomRole
    {
        public override string Name { get; set; } = "深红教会血楔打击组-异教徒";
        public override int Id { get; set; } = 5;
        public override RoleTypeId RoleType { get; set; } = RoleTypeId.ClassD;
        public override List<ItemType> ItemTypes { get; set; } = new List<ItemType>()
        {

        };
        public override float MaxHealthy { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override CustomSpawnPosition SpawnPosition { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override string Description { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
