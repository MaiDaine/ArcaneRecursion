using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArcaneRecursion
{
    public class Gust : CombatSkill
    {
        public override void OnSkillLaunched(SkillDefinition skillDefinition, UnitController caster, CombatCursor cursor, Tile targetTile)
        {
            base.OnSkillLaunched(skillDefinition, caster, cursor, targetTile);

            foreach (Tile tile in cursor.AvailableTiles)
            {
                if (tile.TileEntity != null)
                {
                    HexCoordinates direction = new HexCoordinates(
                        tile.Coordinates.X - caster.CurrentTile.Coordinates.X,
                        tile.Coordinates.Z - caster.CurrentTile.Coordinates.Z
                    );
                    UnitController target = tile.TileEntity.GameObject.GetComponent<UnitController>();

                    if (!target.Ressources.OnHPLoss(skillDefinition.SkillStats.Potency))
                        target.OnKnockBack(direction);
                }
            }
        }
    }
}