using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArcaneRecursion
{
    public class ArcaneFlow : CombatSkill
    {
        public override void OnSkillLaunched(SkillDefinition skillDefinition, UnitController caster, CombatCursor cursor, Tile targetTile)
        {
            caster.Status.ApplyEffect(new ArcaneFlowEffectPassiv());
        }
    }

    public class ArcaneFlowEffectPassiv : CombatEffect
    {
        public ArcaneFlowEffectPassiv()
        {
            base.SetName();
            Duration = -1;
        }

        public override bool OnDispell(UnitController unit)
        {
            return false;
        }

        public override bool OnSkillLaunched(UnitController unit)
        {
            unit.Status.ApplyEffect(new ArcaneFlowEffect());
            return false;
        }
    }

    public class ArcaneFlowEffect : CombatEffect, ISkillEnhancement
    {
        public ArcaneFlowEffect()
        {
            base.SetName();
            Duration = 1;
        }

        public override bool OnSkillLaunched(UnitController unit)
        {
            return true;
        }

        public void ApplyEnhancement(ref SkillModifier castEnhancement)
        {
            castEnhancement.MPPercent -= 0.2f;
        }
    }
}