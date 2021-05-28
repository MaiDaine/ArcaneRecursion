using UnityEngine;

namespace ArcaneRecursion
{
    public class LifeLeech : CombatSkill
    {
        public override void OnSkillLaunched(SkillDefinition skillDefinition, UnitController caster, CombatCursor cursor, Tile targetTile)
        {
            base.OnSkillLaunched(skillDefinition, caster, cursor, targetTile);
            targetTile.TileEntity.GameObject.GetComponent<UnitController>().Status.ApplyEffect(new LifeLeechEffect(skillDefinition.SkillStats.Potency, caster));
        }
    }

    public class LifeLeechEffect : CombatEffect
    {
        private readonly UnitController _caster;
        private readonly string _overgrowthEffectName;

        public LifeLeechEffect(int potency, UnitController caster)
        {
            base.SetName();
            Potency = potency;
            Duration = 3;
            _caster = caster;
            _overgrowthEffectName = SkillLibrary.GetNameFromType(typeof(OvergrowthEffect));
        }

        public override bool OnTurnEnd(UnitController unit)
        {
            int damage = Potency;
            unit.Ressources.OnDamageTaken(ref damage, DamageTypes.Earth);
            if (damage > 0)
            {
                CombatEffect overGrowth = _caster.Skills.GetTrackedEffect(_overgrowthEffectName);

                if (overGrowth != null)
                    (overGrowth as OvergrowthEffect).Target.Ressources.OnHPGain(damage);
                else
                    _caster.Ressources.OnHPGain(damage);
            }

            return false;
        }
    }
}