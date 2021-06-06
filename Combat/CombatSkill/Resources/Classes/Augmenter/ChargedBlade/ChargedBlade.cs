using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArcaneRecursion
{
    public class ChargedBlade : CombatSkill
    {
        public override void OnSkillLaunched(SkillDefinition skillDefinition, UnitController caster, CombatCursor cursor, Tile targetTile)
        {
            caster.Status.ApplyEffect(new ChargedBladeEffectPassive(skillDefinition.SkillStats.Potency));
        }
    }

    public class ChargedBladeEffectPassive : CombatEffect
    {
        public ChargedBladeEffectPassive(int potency)
        {
            base.SetName();
            Potency = potency;
            Duration = PASSIVEFFECT;
        }

        public override bool OnDispell(UnitController unit)
        {
            return false;
        }

        public override bool OnSkillLaunched(UnitController unit)
        {
            if (unit.Status.ActiveEffects.Find(e => e.Name == SkillLibrary.GetNameFromType(typeof(ChargedBladeEffect))) == null)
                unit.Status.ApplyEffect(new ChargedBladeEffect(Potency));
            return false;
        }
    }

    public class ChargedBladeEffect : CombatEffect, IAtkEnhancement
    {
        public ChargedBladeEffect(int potency)
        {
            base.SetName();
            Potency = potency;
            Duration = 1;
        }

        public override bool OnAtkLaunched(UnitController unit, Tile targetTile)
        {
            return true;
        }

        public void ApplyEnhancement(ref SkillModifier atkEnhancement)
        {
            atkEnhancement.Potency.PercentValue += Potency;
        }
    }
}