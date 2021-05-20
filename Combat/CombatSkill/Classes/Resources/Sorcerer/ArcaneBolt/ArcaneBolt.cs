namespace ArcaneRecursion
{
    public class ArcaneBolt : CombatSkill
    {
        public override bool CheckRequirements(SkillDefinition skillDefinition, UnitController unit, Tile targetTile)
        {
            return targetTile?.TileEntity?.Team != 0;
        }

        public override void OnSkillLaunched(SkillDefinition skillDefinition, UnitController caster, CombatCursor cursor, Tile targetTile)
        {
            base.OnSkillLaunched(skillDefinition, caster, cursor, targetTile);
            targetTile.TileEntity.GameObject.GetComponent<UnitController>().Ressources.OnDamageTaken(_updatedStats.Potency, DamageTypes.Arcane);
        }
    }
}