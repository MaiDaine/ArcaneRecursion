using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArcaneRecursion
{
    public class Whirlwind : CombatSkill
    {
        /*
        public override bool CheckRequirements(SkillDefinition skillDefinition, UnitController unit, Tile targetTile)
        {
            return true;
        }
        */

        public override void OnSkillLaunched(SkillDefinition skillDefinition, UnitController caster, CombatCursor cursor, Tile targetTile)
        {
            base.OnSkillLaunched(skillDefinition, caster, cursor, targetTile);

            int targetCount = 0;
            foreach (Tile tile in cursor.AvailableTiles)
                if (tile.TileEntity != null && tile != caster.CurrentTile)
                {
                    tile.TileEntity.GameObject.GetComponent<UnitController>().Ressources.OnDamageTaken(skillDefinition.SkillStats.Potency);
                    targetCount++;
                }

            if (targetCount >= 3)
                caster.Ressources.OnHPGain(skillDefinition.SkillStats.Potency);
        }
    }
}
