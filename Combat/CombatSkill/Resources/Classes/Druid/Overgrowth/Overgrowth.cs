namespace ArcaneRecursion
{
    public class Overgrowth : CombatSkill
    {
        public override bool CheckRequirements(SkillDefinition skillDefinition, UnitController unit, Tile targetTile)
        {
            return targetTile?.TileEntity?.Team != 0;
        }

        public override void OnSkillLaunched(SkillDefinition skillDefinition, UnitController caster, CombatCursor cursor, Tile targetTile)
        {
            base.OnSkillLaunched(skillDefinition, caster, cursor, targetTile);

            UnitController target = targetTile.TileEntity.GameObject.GetComponent<UnitController>();

            target.Status.ApplyEffect(new OvergrowthEffect(_updatedStats.Potency, caster, target));
        }
    }

    public class OvergrowthEffect : CombatEffect
    {
        public UnitController Target;

        public OvergrowthEffect(int potency, UnitController caster, UnitController target)
        {
            base.SetName();
            Duration = 2;
            Potency = potency;
            caster.Skills.AddTrackedEffect(this);
            Target = target;
        }

        public void Effect(UnitController unit)
        {
            unit.Ressources.OnHPGain(Potency);
        }

        public override bool OnDispell(UnitController unit)
        {
            Effect(unit);
            return true;
        }

        public override void OnDurationEnd(UnitController unit)
        {
            Effect(unit);
        }
    }
}
