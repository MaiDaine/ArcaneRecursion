using UnityEngine;

namespace ArcaneRecursion
{
    public class FlameSpears : DirectionalCombatSkill
    {
        public override void FrontAttack(SkillDefinition skillDefinition, UnitController caster, CombatCursor cursor, UnitController targetUnit)
        {
            targetUnit.Ressources.OnDamageTaken(_updatedStats.Potency, DamageTypes.Fire);
        }

        public override void SideAttack(SkillDefinition skillDefinition, UnitController caster, CombatCursor cursor, UnitController targetUnit)
        {
            targetUnit.Ressources.OnDamageTaken((int)(_updatedStats.Potency * 1.5f), DamageTypes.Fire);
            targetUnit.Status.ApplyEffect(new FlameSpearsEffect(_updatedStats.Potency / 2));
        }
        public override void BackAttack(SkillDefinition skillDefinition, UnitController caster, CombatCursor cursor, UnitController targetUnit)
        {
            targetUnit.Ressources.OnDamageTaken(_updatedStats.Potency * 2, DamageTypes.Fire);
            targetUnit.Status.ApplyEffect(new FlameSpearsEffect(_updatedStats.Potency / 2));
        }
    }

    public class FlameSpearsEffect : CombatEffect
    {
        public FlameSpearsEffect(int potency)
        {
            base.SetName();
            Duration = 2;
            Potency = potency;
        }

        public override bool OnTurnEnd(UnitController unit)
        {
            unit.Ressources.OnDamageTaken(Potency, DamageTypes.Fire);
            return false;
        }
    }
}