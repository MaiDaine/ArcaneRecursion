using System;
using UnityEngine;

namespace ArcaneRecursion
{
    public class UnitMovement : MonoBehaviour
    {
        public Tile CurrentTile { get; private set; }
        public HexCoordinates Orientation { get; private set; }

        private const int MOVEANIMATIONSPEED = 10;

        private UnitAnimation _animator;
        private CombatEntity _entity;
        private Action _callback;
        private Tile[] _path;
        private int _currentPathIndex;
        private float _moveDelta;

        #region Init
        public void Init(Tile currentTile, CombatEntity entity)
        {
            CurrentTile = currentTile;
            _entity = entity;

            gameObject.transform.position = currentTile.transform.position;
            CurrentTile.SetTileState(TileState.Occupied);
            CurrentTile.TileEntity = entity;
        }
        #endregion /* Init*/

        public void MoveTo(Action callback, Tile[] path)
        {
            _callback = callback;
            _currentPathIndex = 0;
            _moveDelta = 0f;
            _path = path;
            _animator.PlayAnimation(UnitAnimationType.Run);
        }

        public void SetOrientation(Tile tile)
        {
            Orientation = new HexCoordinates(CurrentTile.Coordinates, tile.Coordinates);
            gameObject.transform.LookAt(tile.transform);
        }

        //TODO Animation
        public void Teleport(Tile destination)
        {
            CurrentTile.TileEntity = null;
            CurrentTile.SetTileState(TileState.Empty);
            CurrentTile = destination;
            destination.TileEntity = _entity;
            transform.position = destination.transform.position;
        }

        #region MonoBehavior LifeCycle
        private void Awake()
        {
            _animator = GetComponent<UnitAnimation>();
        }

        private void FixedUpdate()
        {
            if (_path != null)
            {
                if (Vector3.Distance(transform.position, _path[_currentPathIndex].transform.position) < 0.1f)
                {
                    _moveDelta = 0f;
                    CurrentTile.SetTileState(TileState.Empty);
                    CurrentTile.TileEntity = null;

                    SetOrientation(_path[_currentPathIndex]);
                    CurrentTile = _path[_currentPathIndex];
                    CurrentTile.SetTileState(TileState.Occupied);
                    CurrentTile.TileEntity = _entity;

                    _currentPathIndex++;
                    if (_currentPathIndex == _path.Length)
                    {
                        _path = null;
                        _animator.PlayIndexedAnimation(UnitAnimationType.Idle);
                        _callback();
                        return;
                    }
                }

                _moveDelta += Time.deltaTime * MOVEANIMATIONSPEED;
                transform.position = Vector3.Lerp(CurrentTile.transform.position, _path[_currentPathIndex].transform.position, _moveDelta);
            }
        }
        #endregion /* MonoBehavior LifeCycle */
    }
}