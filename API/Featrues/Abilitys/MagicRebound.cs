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
    public class MagicRebound : CustomAbility
    {
        public override int Id { get; set; } = 2;
        public override string Name { get; set; } = "<mark=blue><color=red>奇术反弹</red></mark>";
        public override string Description { get; set; } = "空间系奇术，用于防御";
        public override AbilityManager.AbilityType AbilityType { get; set; } = AbilityManager.AbilityType.Ability2;
        public override void HandleAbility()
        {
            SchematicObject schematicObject = ObjectSpawner.SpawnSchematic("gocft", Owner.Position);
            schematicObject.gameObject.GetComponent<ReboundGameObject>().Owner = Owner;
            base.HandleAbility();
        }
    }
}
