using AdminToys;
using AutoEvent_5KMode.API.Featrues.GameObjectScripts;
using ProjectMER.Features;
using ProjectMER.Features.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoEvent_5KMode.API.Featrues.Abilitys
{
    public class SpatialRealityAnchoring : CustomAbility
    {
        public override int Id { get; set; } = 3;
        public override string Name { get; set; } = "<mark=blue>空间现实锚定</mark>";
        public override string Description { get; set; } = "放置锚定法阵，让他人无法经过";
        public override AbilityManager.AbilityType AbilityType { get; set; } = AbilityManager.AbilityType.Ability2;
        public override float CoolDown { get; set; } = 120;
        public new float EffectTime = 10;
        public override void HandleAbility()
        {
            SchematicObject schematicObject = ObjectSpawner.SpawnSchematic("RealityAnchoring", base.Owner.Position);
            foreach(WaypointToy waypointToy in schematicObject.AdminToyBases.Where(x => x is WaypointToy))
            {
                waypointToy.transform.gameObject.AddComponent<SpecifiWaypoint>().AllowedHubs.Add(base.Owner.ReferenceHub);
            }
            base.HandleAbility();
        }
    }
}
