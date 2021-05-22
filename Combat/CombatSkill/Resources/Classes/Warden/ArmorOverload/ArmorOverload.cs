using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArcaneRecursion
{
    public class ArmorOverload : CombatSkill
    {
        public override void OnSkillLaunched(SkillDefinition skillDefinition, UnitController caster, CombatCursor cursor, Tile targetTile)
        {
            base.OnSkillLaunched(skillDefinition, caster, cursor, targetTile);
            caster.Status.ApplyEffect(new ArmorOverloadEffect(skillDefinition.SkillStats.Potency));
        }
    }

    public class ArmorOverloadEffect : ShieldCombatEffect
    {
        public ArmorOverloadEffect(int potency)
        {
            base.SetName();
            Potency = potency;
            Duration = 1;
        }

        public override void OnDurationEnd(UnitController unit)
        {
            foreach (Tile tile in unit.CurrentTile.SearchData.Neighbors)
                tile?.TileEntity?.GameObject.GetComponent<UnitController>().Ressources.OnDamageTaken(Potency);
        }
    }
}