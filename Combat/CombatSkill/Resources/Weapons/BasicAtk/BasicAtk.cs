using System;

namespace ArcaneRecursion
{
    public class BasicAtk : DirectionalCombatSkill
    {
        public override void FrontAttack(SkillDefinition skillDefinition, UnitController caster, CombatCursor cursor, UnitController targetUnit)
        {
            targetUnit.Ressources.OnDamageTaken(_updatedStats.Potency, DamageTypes.Physical);
        }

        public override void SideAttack(SkillDefinition skillDefinition, UnitController caster, CombatCursor cursor, UnitController targetUnit)
        {
            targetUnit.Ressources.OnDamageTaken((int)(_updatedStats.Potency * 1.5f), DamageTypes.Physical);
        }

        public override void BackAttack(SkillDefinition skillDefinition, UnitController caster, CombatCursor cursor, UnitController targetUnit)
        {
            targetUnit.Ressources.OnDamageTaken(_updatedStats.Potency * 2, DamageTypes.Physical);
        }
    }
}
