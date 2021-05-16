using System;
using System.Collections.Generic;
using UnityEngine;

namespace ArcaneRecursion
{
    [Serializable]
    public struct TileOffset
    {
        public int X;
        public int Y;
        public int Z;
        public TargetRequireType TargetRequireType;
    }

    [CreateAssetMenu(menuName = "ArcaneRecursion/Combat/SelectionCursor")]
    public class SelectionCursor : ScriptableObject
    {
        public TileOffset[] TileOffsets;
        public TileOffset Movement;
        public SkillMovementType MovementType;

        public Tile[] Apply(CombatGrid grid, UnitController unit, Tile toTile)
        {
            if (TileOffsets.Length == 0)
                return null;

            return MovementType switch
            {
                SkillMovementType.Directional => DirectionalApply(grid, unit.CurrentTile, toTile).ToArray(),
                SkillMovementType.Projectile => PathingApply(grid, unit.CurrentTile, toTile).ToArray(),
                _ => BasicApply(grid, toTile).ToArray(),
            };
        }

        private int CalculateDirection(int from, int to)
        {
            if (from == to)
                return 0;
            return from > to ? -1 : 1;
        }

        private List<Tile> BasicApply(CombatGrid grid, Tile toTile)
        {
            List<Tile> tiles = new List<Tile>();

            for (int i = 0; i < TileOffsets.Length; i++)
            {
                //TODO REQUIRE
                int targetZ = toTile.Coordinates.Z + TileOffsets[i].Z;
                int targetX = toTile.Coordinates.X + TileOffsets[i].X;
                int tmpX = targetX + (targetZ / 2);
                if (tmpX >= 0 && tmpX < grid.Width && targetZ >= 0 && targetZ < grid.Height)
                    tiles.Add(grid.GetTilefromCoordinate(targetX, targetZ));
            }
            foreach (Tile tile in tiles)
                tile.SetTileTmpState(TileTmpState.Select);
            return tiles;
        }

        private List<Tile> DirectionalApply(CombatGrid grid, Tile fromTile, Tile toTile)
        {
            List<Tile> tiles = new List<Tile>();

            int offsetX = CalculateDirection(fromTile.Coordinates.X, toTile.Coordinates.X);
            int offsetZ = CalculateDirection(fromTile.Coordinates.Z, toTile.Coordinates.Z);
            for (int i = 0; i < TileOffsets.Length; i++)
            {
                //TODO REQUIRE
                int targetZ = toTile.Coordinates.Z + (TileOffsets[i].Z * offsetZ);
                int targetX = toTile.Coordinates.X + (TileOffsets[i].X * offsetX);
                int tmpX = targetX + (targetZ / 2);
                if (tmpX >= 0 && tmpX < grid.Width && targetZ >= 0 && targetZ < grid.Height)
                    tiles.Add(grid.GetTilefromCoordinate(targetX, targetZ));
            }
            return tiles;
        }

        private List<Tile> PathingApply(CombatGrid grid, Tile fromTile, Tile toTile)
        {
            List<Tile> tiles = new List<Tile>();
            Tile[] pathToTarget = null;

            tiles.Add(fromTile);
            for (HexDirection d = HexDirection.NE; d <= HexDirection.NW; d++)
            {
                Tile[] tmp = null;
                Tile searchTile = toTile.SearchData.GetNeighbor(d);

                if (searchTile != null)
                {
                    tmp = grid.FindPath(fromTile, searchTile);
                    if (tmp != null)
                    {
                        if (pathToTarget == null)
                            pathToTarget = tmp;
                        else if (pathToTarget.Length > tmp.Length)
                            pathToTarget = tmp;
                    }
                }
            }
            if (pathToTarget == null)
                return null;
            tiles.AddRange(pathToTarget);
            foreach (Tile tile in tiles)
                tile.SetTileTmpState(TileTmpState.Path);

            for (int i = 0; i < TileOffsets.Length; i++)
            {
                //TODO REQUIRE
                int targetZ = toTile.Coordinates.Z + TileOffsets[i].Z;
                int targetX = toTile.Coordinates.X + TileOffsets[i].X;
                int tmpX = targetX + (targetZ / 2);
                if (tmpX >= 0 && tmpX < grid.Width && targetZ >= 0 && targetZ < grid.Height)
                {
                    Tile tile = grid.GetTilefromCoordinate(
                        targetX,
                        targetZ
                    );
                    tiles.Add(tile);
                    tile.SetTileTmpState(TileTmpState.Select);
                }
            }

            return tiles;
        }
    }
}
