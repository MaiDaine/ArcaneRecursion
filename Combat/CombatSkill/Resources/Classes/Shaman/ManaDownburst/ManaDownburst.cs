using UnityEngine;

namespace ArcaneRecursion
{
    public class ManaDownburst : CombatSkill
    {
        public override void OnSkillLaunched(SkillDefinition skillDefinition, UnitController caster, CombatCursor cursor, Tile targetTile)
        {
            base.OnSkillLaunched(skillDefinition, caster, cursor, targetTile);

            foreach (Tile tile in cursor.AvailableTiles)
                tile.TileEntity?.GameObject.GetComponent<UnitController>().Ressources.OnHPGain(skillDefinition.SkillStats.Potency);
        }
    }
}