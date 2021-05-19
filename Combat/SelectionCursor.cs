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
        public SkillCursorType MovementType;

        public Tile[] Apply(CombatGrid grid, UnitController unit, Tile toTile, int unitTeamId)
        {
            if (TileOffsets.Length == 0)
                return null;

            return MovementType switch
            {
                SkillCursorType.Directional => DirectionalApply(grid, unit.CurrentTile, toTile, unitTeamId),
                SkillCursorType.Projectile => PathingApply(grid, unit.CurrentTile, toTile, unitTeamId),
                SkillCursorType.Radial => RadialApply(grid, unit.CurrentTile, toTile, unitTeamId),
                _ => BasicApply(grid, toTile, unitTeamId),
            };
        }

        private int CalculateDirection(int from, int to)
        {
            if (from == to)
                return 0;
            return from > to ? -1 : 1;
        }

        private bool ValidateTileState(Tile tile, TargetRequireType type, int unitTeamId)
        {
            return type switch
            {
                TargetRequireType.Any => true,
                TargetRequireType.Valid => tile.State != TileState.Invalid,
                TargetRequireType.Empty => tile.State == TileState.Empty,
                TargetRequireType.Unit => tile.TileEntity != null,
                TargetRequireType.Allied => tile.TileEntity?.Team == unitTeamId,
                TargetRequireType.Enemy => tile.TileEntity?.Team != 0 && tile.TileEntity?.Team != unitTeamId,
                _ => true,
            };
        }

        private Tile[] BasicApply(CombatGrid grid, Tile toTile, int unitTeamId)
        {
            List<Tile> tiles = new List<Tile>();

            for (int i = 0; i < TileOffsets.Length; i++)
            {
                int targetZ = toTile.Coordinates.Z + TileOffsets[i].Z;
                int targetX = toTile.Coordinates.X + TileOffsets[i].X;
                int tmpX = targetX + (targetZ / 2);
                if (tmpX >= 0 && tmpX < grid.Width && targetZ >= 0 && targetZ < grid.Height)
                {
                    Tile tile = grid.GetTilefromCoordinate(targetX, targetZ);
                    if (ValidateTileState(tile, TileOffsets[i].TargetRequireType, unitTeamId))
                        tiles.Add(tile);
                    else
                        return null;
                }
            }
            foreach (Tile t in tiles)
                t.SetTileTmpState(TileTmpState.Select);
            return tiles.ToArray();
        }

        private Tile[] DirectionalApply(CombatGrid grid, Tile fromTile, Tile toTile, int unitTeamId)
        {
            List<Tile> tiles = new List<Tile>();

            int offsetX = CalculateDirection(fromTile.Coordinates.X, toTile.Coordinates.X);
            int offsetZ = CalculateDirection(fromTile.Coordinates.Z, toTile.Coordinates.Z);
            Debug.Log(offsetX + " " + offsetZ);
            for (int i = 0; i < TileOffsets.Length; i++)
            {
                int targetX = toTile.Coordinates.X + (TileOffsets[i].X * offsetX);
                int targetZ = toTile.Coordinates.Z + (TileOffsets[i].Z * offsetZ);
                int tmpX = targetX + (targetZ / 2);
                if (tmpX >= 0 && tmpX < grid.Width && targetZ >= 0 && targetZ < grid.Height)
                {
                    Tile tile = grid.GetTilefromCoordinate(targetX, targetZ);
                    if (ValidateTileState(tile, TileOffsets[i].TargetRequireType, unitTeamId))
                        tiles.Add(tile);
                    else
                        return null;
                }
            }
            foreach (Tile t in tiles)
                t.SetTileTmpState(TileTmpState.Select);
            return tiles.ToArray();
        }

        private Tile[] PathingApply(CombatGrid grid, Tile fromTile, Tile toTile, int unitTeamId)
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
                int targetZ = toTile.Coordinates.Z + TileOffsets[i].Z;
                int targetX = toTile.Coordinates.X + TileOffsets[i].X;
                int tmpX = targetX + (targetZ / 2);
                if (tmpX >= 0 && tmpX < grid.Width && targetZ >= 0 && targetZ < grid.Height)
                {
                    Tile tile = grid.GetTilefromCoordinate(targetX, targetZ);
                    if (ValidateTileState(tile, TileOffsets[i].TargetRequireType, unitTeamId))
                        tiles.Add(tile);
                    else
                        return null;
                }
            }
            foreach (Tile t in tiles)
                t.SetTileTmpState(TileTmpState.Select);
            return tiles.ToArray();
        }

        //Base position is NE
        private Tile[] RadialApply(CombatGrid grid, Tile fromTile, Tile toTile, int unitTeamId)
        {
            Func<HexCoordinates, HexCoordinates> getRotation = null;
            List<Tile> tiles = new List<Tile>();

            for (int i = 0; i < 6; i++)
                if (fromTile.SearchData.Neighbors[i] == toTile)
                {
                    getRotation = (HexDirection)i switch
                    {
                        HexDirection.NE => null,
                        HexDirection.E => HexCoordinates.RotateClockwise60,
                        HexDirection.SE => HexCoordinates.RotateClockwise120,
                        HexDirection.SW => HexCoordinates.Rotate180,
                        HexDirection.W => HexCoordinates.RotateCounterClockwise120,
                        HexDirection.NW => HexCoordinates.RotateCounterClockwise60,
                        _ => null,
                    };
                    break;
                }

            for (int i = 0; i < TileOffsets.Length; i++)
            {
                HexCoordinates offset = new HexCoordinates(TileOffsets[i].X, TileOffsets[i].Z);
                if (getRotation != null)
                    offset = getRotation(offset);
                HexCoordinates targetOffset = new HexCoordinates(toTile.Coordinates.X + offset.X, toTile.Coordinates.Z + offset.Z);

                int tmpX = targetOffset.X + (targetOffset.Z / 2);
                if (tmpX >= 0 && tmpX < grid.Width && targetOffset.Z >= 0 && targetOffset.Z < grid.Height)
                {
                    Tile tile = grid.GetTilefromCoordinate(targetOffset.X, targetOffset.Z);
                    if (ValidateTileState(tile, TileOffsets[i].TargetRequireType, unitTeamId))
                        tiles.Add(tile);
                }
            }
            foreach (Tile t in tiles)
                t.SetTileTmpState(TileTmpState.Select);
            return tiles.ToArray();
        }
    }
}
