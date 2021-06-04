using System.Collections.Generic;
using UnityEngine;

namespace ArcaneRecursion
{
    public class Tile : MonoBehaviour
    {
        public HexCoordinates Coordinates { get; private set; }
        public List<CombatEffect> OnTileMovementEffects { get; private set; }
        public TileSearchData SearchData { get; private set; }//TODO Isolate neighbors after init
        public TileState State { get; private set; }
        public TileTmpState TmpState { get; private set; } = TileTmpState.None;
        public CombatEntity TileEntity { get; set; }
        public int MoveCostPercent { get; set; }

        [SerializeField] private MeshRenderer tileRenderer;

        public void Init(HexCoordinates coordinates, TileState state = TileState.Empty)
        {
            Coordinates = coordinates;
            text.text = coordinates.ToStringOnSeparateLines();
            SearchData = new TileSearchData(this);
            SetTileState(state);
            OnTileMovementEffects = new List<CombatEffect>();
            MoveCostPercent = 100;
        }

        #region TileState
        public void SetTileState(TileState state)
        {
            State = state;
            switch (state)
            {
                case TileState.None:
                    break;
                case TileState.Invalid:
                    tileRenderer.material = CombatLibrary.Instance.TileMaterials[(int)TileMaterial.Invalid];
                    break;
                case TileState.Empty:
                    tileRenderer.material = CombatLibrary.Instance.TileMaterials[(int)TileMaterial.Default];
                    break;
                case TileState.Occupied:
                    tileRenderer.material = CombatLibrary.Instance.TileMaterials[(int)TileMaterial.Invalid];
                    break;
                default:
                    tileRenderer.material = CombatLibrary.Instance.TileMaterials[(int)TileMaterial.Default];
                    break;
            }
        }

        public void SetTileTmpState(TileTmpState state)
        {
            TmpState = state;
            switch (state)
            {
                case TileTmpState.None:
                    SetTileState(State);
                    break;
                case TileTmpState.Invalid:
                    tileRenderer.material = CombatLibrary.Instance.TileMaterials[(int)TileMaterial.Invalid];
                    break;
                case TileTmpState.Select:
                    tileRenderer.material = CombatLibrary.Instance.TileMaterials[(int)TileMaterial.Select];
                    break;
                case TileTmpState.Hover:
                    tileRenderer.material = CombatLibrary.Instance.TileMaterials[(int)TileMaterial.Hover];
                    break;
                case TileTmpState.Path:
                    tileRenderer.material = CombatLibrary.Instance.TileMaterials[(int)TileMaterial.Path];
                    break;
                case TileTmpState.SkillRange:
                    tileRenderer.material = CombatLibrary.Instance.TileMaterials[(int)TileMaterial.SkillTarget];
                    break;
                default:
                    tileRenderer.material = CombatLibrary.Instance.TileMaterials[(int)TileMaterial.Default];
                    break;
            }
        }
        #endregion /* TileState */

        public void OnUnitEnterTile(UnitController unit, bool teleport)
        {
            if (OnTileMovementEffects.Count > 0)
                foreach (CombatEffect effect in OnTileMovementEffects)
                    effect.OnUnitEnterTile(unit, this);
        }

        public void OnUnitExitTile(UnitController unit, bool teleport)
        {
            if (OnTileMovementEffects.Count > 0)
                foreach (CombatEffect effect in OnTileMovementEffects)
                    effect.OnUnitExitTile(unit, this);
        }

        #region DEBUG
        [SerializeField] private TextMesh text;

        public void ToggleText(bool state)
        {
            text.gameObject.SetActive(state);
        }

        public void SetText(string s)
        {
            text.text = s;
        }
        #endregion /* DEBUG */

    }
}