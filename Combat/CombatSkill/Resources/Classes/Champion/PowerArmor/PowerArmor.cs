using UnityEngine;

namespace ArcaneRecursion
{
    public class PowerArmor : CombatSkill
    {
        public override void OnSkillLaunched(SkillDefinition skillDefinition, UnitController caster, CombatCursor cursor, Tile targetTile)
        {
            base.OnSkillLaunched(skillDefinition, caster, cursor, targetTile);
            caster.Status.ApplyEffect(new PowerArmorEffect(skillDefinition.SkillStats.Potency));
        }
    }

    public class PowerArmorEffect : CombatEffect, IDefEnhancement, IAtkEnhancement
    {
        private int stack = 3;

        public PowerArmorEffect(int potency)
        {
            base.SetName();
            Duration = 2;
            Potency = potency;
        }

        public override bool OnAtkLaunched(UnitController unit, Tile targetTile)
        {
            stack--;
            return stack == 0;
        }

        public void ApplyEnhancement(ref DefModifier defEnhancement)
        {
            defEnhancement.Physical.PercentValue += Potency * stack;
        }

        public void ApplyEnhancement(ref SkillModifier atkEnhancement)
        {
            atkEnhancement.Potency.FlatValue += Potency * stack;
        }
    }
}
