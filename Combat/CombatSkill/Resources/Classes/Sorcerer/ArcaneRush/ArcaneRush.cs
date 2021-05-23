namespace ArcaneRecursion
{
    public class ArcaneRush : CombatSkill
    {
        public override bool CheckRequirements(SkillDefinition skillDefinition, UnitController unit, Tile targetTile)
        {
            if (unit.Status.StatusSummary.IsRoot)
                return false;
            return base.CheckRequirements(skillDefinition, unit, targetTile);
        }

        public override void OnSkillLaunched(SkillDefinition skillDefinition, UnitController caster, CombatCursor cursor, Tile targetTile)
        {
            base.OnSkillLaunched(skillDefinition, caster, cursor, targetTile);

            caster.Movement.Teleport(targetTile);
            foreach (Tile tile in cursor.AvailableTiles)
                tile?.TileEntity?.GameObject.GetComponent<UnitController>().Ressources.OnDamageTaken(skillDefinition.SkillStats.Potency, DamageTypes.Arcane);
        }
    }
}