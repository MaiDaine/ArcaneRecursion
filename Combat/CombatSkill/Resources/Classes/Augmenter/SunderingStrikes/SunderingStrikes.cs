using UnityEngine;

namespace ArcaneRecursion
{
    public class SunderingStrikes : CombatSkill
    {
        public override void OnSkillLaunched(SkillDefinition skillDefinition, UnitController caster, CombatCursor cursor, Tile targetTile)
        {
            caster.Status.ApplyEffect(new SunderingStrikesEffectBuff());
        }
    }

    public class SunderingStrikesEffectBuff : CombatEffect
    {
        public SunderingStrikesEffectBuff()
        {
            base.SetName();
            Duration = 2;
        }

        public override bool OnAtkLaunched(UnitController unit, Tile targetTile)
        {
            UnitController target = targetTile.TileEntity.GameObject.GetComponent<UnitController>();

            if (target.Status.ActiveEffects.FindAll(e => e.Name == Name).Count < 3)
                target.Status.ApplyEffect(new SunderingStrikesEffectDebuff());

            return false;
        }
    }

    public class SunderingStrikesEffectDebuff : CombatEffect, IDefEnhancement
    {
        public SunderingStrikesEffectDebuff()
        {
            base.SetName();
            Duration = 1;
        }

        public void ApplyEnhancement(ref DefModifier defEnhancement)
        {
            defEnhancement.Physical.PercentValue -= 5;
        }
    }
}