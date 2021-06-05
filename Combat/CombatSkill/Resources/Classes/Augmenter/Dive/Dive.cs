using UnityEngine;

namespace ArcaneRecursion
{
    public class Dive : CombatSkill
    {
        public override bool CheckRequirements(SkillDefinition skillDefinition, UnitController unit, Tile targetTile)
        {
            if (unit.Status.StatusSummary.IsRoot)
                return false;
            return base.CheckRequirements(skillDefinition, unit, targetTile);
        }

        public override void OnSkillLaunched(SkillDefinition skillDefinition, UnitController caster, CombatCursor cursor, Tile targetTile)
        {
            Tile destinationtile = cursor.AvailableTiles[1];
            HexCoordinates targetOrientation = targetTile.TileEntity.GameObject.GetComponent<UnitMovement>().Orientation;
            HexCoordinates offset = new HexCoordinates(targetTile.Coordinates.X - targetOrientation.X, targetTile.Coordinates.Z - targetOrientation.Z);

            Debug.Log(targetOrientation.ToString());
            Debug.Log(offset.ToString());
            Debug.Log(destinationtile.Coordinates.ToString());
            if (caster.CurrentTile.Coordinates.X == offset.X && caster.CurrentTile.Coordinates.Z == offset.Z)
                targetTile.TileEntity.GameObject.GetComponent<UnitController>().Status.ApplyEffect(new DiveEffect());
            caster.Movement.Teleport(destinationtile);
        }
    }

    public class DiveEffect : CombatEffect, IUnitStatus
    {
        public DiveEffect()
        {
            base.SetName();
            Duration = 1;
        }

        public void ApplyStatus(ref UnitStatusEffect statusEffect)
        {
            statusEffect.IsRoot = true;
        }
    }
}
