﻿namespace ArcaneRecursion
{
    public class CombatCursor
    {
        public Tile[] AvailableTiles;

        private readonly CombatGrid _grid;

        public CombatCursor(CombatGrid grid)
        {
            _grid = grid;
        }

        public void ClearPrevious()
        {
            if (AvailableTiles != null)
                foreach (Tile pTile in AvailableTiles)
                    pTile.SetTileTmpState(TileTmpState.None);
        }

        public Tile[] UpdateMoveCursor(UnitController unit, Tile fromTile, Tile toTile)
        {
            Tile[] path = null;

            fromTile?.SetTileTmpState(TileTmpState.None);
            ClearPrevious();
            if (toTile == null)
                return null;
            if (toTile.State != TileState.Empty)
                return path;
            toTile.SetTileTmpState(TileTmpState.Hover);
            if (unit != null)
            {
                path = _grid.FindPath(unit.CurrentTile, toTile);
                AvailableTiles = (Tile[])path.Clone();
                if (path != null && unit.CanMoveTo(path))
                    for (int i = 0; i < AvailableTiles.Length - 1; i++)
                        AvailableTiles[i].SetTileTmpState(TileTmpState.Path);
            }
            return path;
        }

        public void UpdateSkillCursor(CombatSkill loadedSkill, SkillData skillData, UnitController unit, Tile toTile)
        {
            if (AvailableTiles != null)
                foreach (Tile pTile in AvailableTiles)
                    pTile.SetTileTmpState(TileTmpState.None);
            AvailableTiles = null;
            foreach (Tile t in loadedSkill.TilesAffected)
                t.SetTileTmpState(TileTmpState.SkillRange);

            if (toTile == null)
                return;

            if (skillData.SkillDefinition.SkillStats.CastRange == 0)
            {
                unit.CurrentTile.SetTileTmpState(TileTmpState.SkillRange);
                if (unit.CurrentTile != toTile)
                {
                    toTile.SetTileTmpState(TileTmpState.Invalid);
                    AvailableTiles = new Tile[2] { unit.CurrentTile, toTile };
                }
                else
                    AvailableTiles = skillData.SkillDefinition.Cursor.Apply(_grid, unit, toTile);
                //AvailableTiles = new Tile[1] { unit.CurrentTile };
                return;
            }

            if (unit.CurrentTile.Coordinates.DistanceTo(toTile.Coordinates) <= skillData.SkillDefinition.SkillStats.CastRange
                && loadedSkill.CheckRequirements(skillData.SkillDefinition, unit, toTile))
                AvailableTiles = skillData.SkillDefinition.Cursor.Apply(_grid, unit, toTile);
            else
            {
                toTile.SetTileTmpState(TileTmpState.Invalid);
                AvailableTiles = new Tile[1] { toTile };
            }
        }
    }
}
