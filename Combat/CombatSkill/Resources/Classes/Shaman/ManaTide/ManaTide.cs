using UnityEngine;

namespace ArcaneRecursion
{
    public class ManaTide : CombatSkill
    {
        public override void OnSkillLaunched(SkillDefinition skillDefinition, UnitController caster, CombatCursor cursor, Tile targetTile)
        {
            caster.Status.ApplyEffect(new ManaTideEffectPassive(skillDefinition.SkillStats.Potency));
        }
    }

    public class ManaTideEffectPassive : CombatEffect
    {
        private bool _active = false;

        public ManaTideEffectPassive(int potency)
        {
            base.SetName();
            Potency = potency;
            Duration = PASSIVEFFECT;
        }

        public override bool OnDispell(UnitController unit)
        {
            return false;
        }

        public override bool OnTurnStart(UnitController unit)
        {
            _active = true;
            return false;
        }

        public override bool OnUnitEnterTile(UnitController unit, Tile tile)
        {
            if (_active)
            {
                _active = false;
                unit.Status.ApplyEffect(new ManaTideEffect(Potency));
            }
            return false;
        }
    }

    public class ManaTideEffect : CombatEffect, ISkillEnhancement
    {
        public ManaTideEffect(int potency)
        {
            base.SetName();
            Potency = potency;
            Duration = 1;
        }

        public override bool OnSkillLaunched(UnitController unit)
        {
            return true;
        }

        public void ApplyEnhancement(ref SkillModifier castEnhancement)
        {
            castEnhancement.Potency.PercentValue += 20;
        }
    }
}
