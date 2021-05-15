namespace ArcaneRecursion
{
    public class Overgrowth : CombatSkill
    {
        public override bool CheckRequirements(SkillDefinition skillDefinition, UnitController unit, Tile targetTile)
        {
            return targetTile?.TileEntity.Team != 0;
        }

        public override void OnSkillLaunched(SkillDefinition skillDefinition, UnitController caster, CombatCursor cursor, Tile targetTile)
        {
            base.OnSkillLaunched(skillDefinition, caster, cursor, targetTile);
            targetTile.TileEntity.GameObject.GetComponent<UnitController>().Status.ApplyEffect(new OvergrowthEffect(_updatedStats.Potency));
        }
    }

    public class OvergrowthEffect : CombatEffect
    {
        public OvergrowthEffect(int potency)
        {
            base.SetName();
            Duration = 2;
            Potency = potency;
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
