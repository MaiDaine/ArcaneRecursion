using UnityEngine;

namespace ArcaneRecursion
{
    public class Cleansing : CombatSkill
    {
        public override void OnSkillLaunched(SkillDefinition skillDefinition, UnitController caster, CombatCursor cursor, Tile targetTile)
        {
            base.OnSkillLaunched(skillDefinition, caster, cursor, targetTile);
            targetTile.TileEntity.GameObject.GetComponent<UnitController>().Status.OnDispell();
        }
    }
}