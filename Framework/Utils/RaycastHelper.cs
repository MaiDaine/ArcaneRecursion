using UnityEngine;
using UnityEngine.InputSystem;

namespace ArcaneRecursion
{
    public class RaycastHelper : MonoBehaviour
    {
        private Camera _mainCamera;
        private LiveShapeDrawer _shapeDrawer;

        private const int PROJECTILEOFFSETY = 1;

        private void Start()
        {
            _mainCamera = GetComponent<Camera>();
            _shapeDrawer = GetComponent<LiveShapeDrawer>();
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
                    tile = unit.CurrentTile;
                else
                {
                    tile = hit.collider.gameObject.GetComponentInParent<Tile>();
                    if (tile?.TileEntity != null)
                        unit = tile.TileEntity.GameObject.GetComponent<UnitController>();
                }
            }
        }

        public void UpdateProjectilePrediction(UnitController caster, out UnitController unit, Tile target)
        {
            Vector3 linetarget = target.transform.position;
            Vector3 start = caster.CurrentTile.transform.position;
            start.y += PROJECTILEOFFSETY;

            Ray ray = new Ray(start, (target.transform.position - start).normalized);
            unit = null;
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, 1 << Layers.UnitLayer))
            {
                unit = hit.collider.gameObject.GetComponent<UnitController>();
                if (unit != null)
                {
                    linetarget = unit.CurrentTile.transform.position;
                    linetarget.y = +PROJECTILEOFFSETY;
                }
            }
            _shapeDrawer.SetLineDraw(start, linetarget);
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