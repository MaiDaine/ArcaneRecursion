using System.Collections.Generic;
using UnityEngine;

namespace ArcaneRecursion
{
    public class CombatGrid : MonoBehaviour
    {
        public int Width = 6;
        public int Height = 6;
        public Tile[] Tiles { get; private set; }

        [SerializeField] private Tile _tilePrefab;
        [SerializeField] private GameEvent _onMapLoaded;
        private TilePriorityQueue _searchFrontier;

        public Tile GetTilefromCoordinate(int x, int z)
        {
            return Tiles[x + (z * Width) + (z / 2)];
        }

        public Tile[] FindPath(Tile startCell, Tile endCell, bool stopBeforeLast = false)
        {
            Tile current;
            List<Tile> path = new List<Tile>();
            _searchFrontier.Clear();

            for (int i = 0; i < Tiles.Length; i++)
                Tiles[i].SearchData.Distance = int.MaxValue;

            startCell.SearchData.Distance = 0;
            _searchFrontier.Enqueue(startCell);
            int index = -1;
            while (_searchFrontier.Count > 0)
            {
                index++;
                current = _searchFrontier.Dequeue();

                if (current == endCell)
                {
                    path.Add(endCell);
                    goto Resolve;
                }

                for (HexDirection d = HexDirection.NE; d <= HexDirection.NW; d++)
                {
                    Tile neighbor = current.SearchData.GetNeighbor(d);
                    if (neighbor == null)
                        continue;
                    if (stopBeforeLast == true && neighbor == endCell)
                    {
                        path.Add(current);
                        goto Resolve;
                    }
                    if (neighbor.State != TileState.Empty)
                        continue;

                    int distance = current.SearchData.Distance;

                    if (neighbor.SearchData.Distance == int.MaxValue)
                    {
                        neighbor.SearchData.Distance = distance;
                        neighbor.SearchData.PathFrom = current;
                        neighbor.SearchData.SearchHeuristic = neighbor.Coordinates.DistanceTo(endCell.Coordinates);
                        _searchFrontier.Enqueue(neighbor);
                    }
                    else if (distance < neighbor.SearchData.Distance)
                    {
                        int oldPriority = neighbor.SearchData.SearchPriority;
                        neighbor.SearchData.Distance = distance;
                        neighbor.SearchData.PathFrom = current;
                        _searchFrontier.Change(neighbor, oldPriority);
                    }
                }
            }

            return null;

        Resolve:
            current = current.SearchData.PathFrom;
            while (current != startCell)
            {
                path.Add(current);
                current = current.SearchData.PathFrom;
            }
            path.Reverse();
            return path.ToArray();
        }

        #region MonoBehavior LifeCycle
        private void Start()
        {
            Tiles = new Tile[Height * Width];

            for (int z = 0, i = 0; z < Height; z++)
            {
                for (int x = 0; x < Width; x++)
                    CreateTile(x, z, i++);
            }
            _searchFrontier = new TilePriorityQueue();
            _onMapLoaded.Raise();
        }
        #endregion /* MonoBehavior LifeCycle */

        private void CreateTile(int x, int z, int i)
        {
            Vector3 position;
            position.x = (x + (z * 0.5f) - (z / 2)) * (HexMetrics.InnerRadius * 2f);
            position.y = 0f;
            position.z = z * (HexMetrics.OuterRadius * 1.5f);

            Tile tile = Tiles[i] = Instantiate<Tile>(_tilePrefab);
            tile.transform.SetParent(transform, false);
            tile.transform.localPosition = position;
            tile.Init(HexCoordinates.FromOffsetCoordinates(x, z));

            if (x > 0)
                tile.SearchData.AddNeighbor(HexDirection.W, Tiles[i - 1]);
            if (z > 0)
            {
                if ((z & 1) == 0)
                {
                    tile.SearchData.AddNeighbor(HexDirection.SE, Tiles[i - Width]);
                    if (x > 0)
                        tile.SearchData.AddNeighbor(HexDirection.SW, Tiles[i - Width - 1]);
                }
                else
                {
                    tile.SearchData.AddNeighbor(HexDirection.SW, Tiles[i - Width]);
                    if (x < Width - 1)
                        tile.SearchData.AddNeighbor(HexDirection.SE, Tiles[i - Width + 1]);
                }
            }
        }
    }
}