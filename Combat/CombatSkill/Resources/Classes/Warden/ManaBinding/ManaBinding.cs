using UnityEngine;

namespace ArcaneRecursion
{
    public class ManaBinding : CombatSkill
    {
        public override void OnSkillLaunched(SkillDefinition skillDefinition, UnitController caster, CombatCursor cursor, Tile targetTile)
        {
            base.OnSkillLaunched(skillDefinition, caster, cursor, targetTile);
            caster.Status.ApplyEffect(new ManaBindingEffect());
        }
    }

    public class ManaBindingEffect : CombatEffect
    {
        public ManaBindingEffect()
        {
            base.SetName();
            Duration = 1;
        }

        public override bool OnAtkLaunched(UnitController unit, Tile targetTile)
        {
            UnitController targetUnit = targetTile.TileEntity.GameObject.GetComponent<UnitController>();

            if (targetUnit != null)
            {
                BasicOrientation fromOrientation = HexCoordinates.GetOrientation(unit.Movement.Orientation, targetUnit.Movement.Orientation);
                BasicOrientation toOrientation = targetUnit.Status.OnDirectionalAttackReceived(fromOrientation);

                targetUnit.Status.ApplyEffect(new ManaBindingEffectRoot());

                if (toOrientation == BasicOrientation.Back)
                    targetUnit.Status.ApplyEffect(new ManaBindingEffectCripple());
            }
            return true;
        }
    }

    public class ManaBindingEffectRoot : CombatEffect, IUnitStatus
    {
        public ManaBindingEffectRoot()
        {
            base.SetName();
            Duration = 1;
        }

        public void ApplyStatus(ref UnitStatusEffect statusEffect)
        {
            statusEffect.IsRoot = true;
        }
    }

    public class ManaBindingEffectCripple : CombatEffect, IUnitStatus
    {
        public ManaBindingEffectCripple()
        {
            base.SetName();
            Duration = 1;
        }

        public void ApplyStatus(ref UnitStatusEffect statusEffect)
        {
            statusEffect.IsCripple = true;
        }
    }
}