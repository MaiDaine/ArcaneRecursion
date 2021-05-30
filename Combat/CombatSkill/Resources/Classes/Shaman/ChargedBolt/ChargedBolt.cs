using UnityEngine;

namespace ArcaneRecursion
{
    public class ChargedBolt : CombatSkill
    {
        public override void OnSkillLaunched(SkillDefinition skillDefinition, UnitController caster, CombatCursor cursor, Tile targetTile)
        {
            base.OnSkillLaunched(skillDefinition, caster, cursor, targetTile);
            targetTile?.TileEntity.GameObject.GetComponent<UnitController>().Ressources.OnDamageTaken(skillDefinition.SkillStats.Potency, DamageTypes.Wind);
        }
    }
}