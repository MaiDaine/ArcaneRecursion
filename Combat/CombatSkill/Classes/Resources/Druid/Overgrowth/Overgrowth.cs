namespace ArcaneRecursion
{
    public class Overgrowth : CombatSkill
    {
        public override bool CheckRequirements(UnitController unit, CombatSkillObject data, Tile targetTile)
        {
            return targetTile?.TileEntity.Team != 0;
        }

        public override void OnSkillLaunched(UnitController caster, CombatSkillObject data, CombatCursor cursor, Tile targetTile)
        {
            base.OnSkillLaunched(caster, data, cursor, targetTile);
            targetTile.TileEntity.GameObject.GetComponent<UnitController>().Status.ApplyEffect(new OvergrowthEffect());
        }
    }

    public class OvergrowthEffect : CombatEffect
    {
        public OvergrowthEffect()
        {
            Name = "Overgrowth";
            Duration = 2;
        }

        public void Effect(UnitController unit)
        {
            unit.Ressources.OnHPGain(100);//TODO SCALE AP
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
