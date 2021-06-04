using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArcaneRecursion
{
    public class Symbiosis : CombatSkill
    {
        public override void OnSkillLaunched(SkillDefinition skillDefinition, UnitController caster, CombatCursor cursor, Tile targetTile)
        {
            caster.Status.ApplyEffect(new SymbiosisEffectPassive(skillDefinition.SkillStats.Potency));
        }
    }

    public class SymbiosisEffectPassive : CombatEffect
    {
        public SymbiosisEffectPassive(int potency)
        {
            base.SetName();
            Duration = PASSIVEFFECT;
            Potency = potency;
        }

        public override bool OnDispell(UnitController unit)
        {
            return false;
        }

        public override bool OnTurnEnd(UnitController unit)
        {
            foreach (Tile tile in unit.CurrentTile.SearchData.Neighbors)
                if (tile?.TileEntity?.Team == unit.CombatEntity.Team)
                {
                    unit.Ressources.OnMPGain(Potency);
                    return false;
                }

            return false;
        }
    }
}