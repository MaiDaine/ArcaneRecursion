namespace ArcaneRecursion
{
    public class ArcaneBolt : CombatSkill
    {
        public override bool CheckRequirements(UnitController unit, CombatSkillObject data, Tile targetTile)
        {
            return targetTile?.TileEntity.Team != 0;
        }

        public override void OnSkillLaunched(UnitController caster, CombatSkillObject data, CombatCursor cursor, Tile targetTile)
        {
            base.OnSkillLaunched(caster, data, cursor, targetTile);
            targetTile.TileEntity.GameObject.GetComponent<UnitController>().Ressources.OnHPLoss(50, DamageTypes.Arcane);//TODO SCALE AP
        }
    }
}