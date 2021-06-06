using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ArcaneRecursion
{
    public class UnstoppableForce : CombatSkill
    {
        public override void OnSkillLaunched(SkillDefinition skillDefinition, UnitController caster, CombatCursor cursor, Tile targetTile)
        {
            caster.Status.ApplyEffect(new UnstoppableForceEffectPassive());
        }
    }

    public class UnstoppableForceEffectPassive : CombatEffect
    {
        public UnstoppableForceEffectPassive()
        {
            base.SetName();
            Duration = PASSIVEFFECT;
        }

        public override bool OnDispell(UnitController unit)
        {
            return false;
        }

        public override bool OnEffectApply(UnitController unit, ref CombatEffect effect, List<SkillTag> skillTags)
        {
            if (skillTags.Contains(SkillTag.Control) && effect.Duration > 1)
                effect.Duration--;

            return true;
        }
    }
}