using UnityEngine;
using UnityEngine.InputSystem;

namespace ArcaneRecursion
{
    public class RaycastHelper : MonoBehaviour
    {
        private Camera _mainCamera;

        private void Start()
        {
            _mainCamera = GetComponent<Camera>();
        }

        public Tile GetTileFromCursor()
        {
            Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, 1 << Layers.GridLayer))
                return hit.collider.gameObject.GetComponentInParent<Tile>();
            return null;
        }

        public void GetEntityFromCursor(out UnitController unit, out Tile tile)
        {
            Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            unit = null;
            tile = null;

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ((1 << Layers.GridLayer) | (1 << Layers.UnitLayer))))
            {
                unit = hit.collider.gameObject.GetComponent<UnitController>();
                if (unit != null)
                {
                    tile = unit.CurrentTile;
                }
                else
                {
                    tile = hit.collider.gameObject.GetComponentInParent<Tile>();
                    if (tile?.TileEntity != null)
                        unit = tile.TileEntity.GameObject.GetComponent<UnitController>();
                }
            }
        }

        public Tile GetNearestTileFromCursor()
        {
            Tile tile = null;
            Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, 1 << Layers.GridLayer))
                tile = hit.collider.gameObject.GetComponentInParent<Tile>();

            return tile;
        }
    }
}