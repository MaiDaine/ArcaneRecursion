using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArcaneRecursion
{
    public class ArcaneFlow : CombatSkill
    {
        public override void OnSkillLaunched(UnitController caster, CombatSkillObject data, CombatCursor cursor, Tile targetTile)
        {
            caster.Status.ApplyEffect(new ArcaneFlowPassivEffect());
        }
    }

    public class ArcaneFlowPassivEffect : CombatEffect
    {
        public ArcaneFlowPassivEffect()
        {
            Name = "ArcaneFlowPassivEffect";
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
            Name = "ArcaneFlowEffect";
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