using UnityEngine;

namespace ArcaneRecursion
{
    public class ChargedBolt : CombatSkill
    {
        public override void OnSkillLaunched(SkillDefinition skillDefinition, UnitController caster, CombatCursor cursor, Tile targetTile)
        {
            base.OnSkillLaunched(skillDefinition, caster, cursor, targetTile);
            int damage = skillDefinition.SkillStats.Potency * (100 + (caster.CurrentTile.Coordinates.DistanceTo(targetTile.Coordinates) * 5)) / 100;

            targetTile?.TileEntity.GameObject.GetComponent<UnitController>().Ressources.OnDamageTaken(damage, DamageTypes.Wind);
        }
    }
}