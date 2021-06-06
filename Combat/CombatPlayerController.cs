using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArcaneRecursion
{
    public class CombatPlayerController : CombatController
    {
        public bool PlayerTurn { get; set; }

        [SerializeField] private RaycastHelper _raycast;
        [SerializeField] private LiveShapeDrawer _shapeDrawer;

        private Tile _currentTile = null;
        private Tile _targetTile = null;
        private bool _canInteract = false;
        private CombatSkill _loadedSkill = null;
        private bool _loadedSkillIsProjectile = false;

        private void LateUpdate()
        {
            if (_canInteract)
                UpdateCursorPosition();
        }

        private void UnlockInteraction() { _canInteract = true; }

        private void UpdateCursorPosition()
        {
            _raycast.GetEntityFromCursor(out UnitController unit, out _targetTile);
            if (_targetTile != _currentTile)
            {
                if (_currentUnit.Skills.SelectedSkill != null)
                {
                    if (_loadedSkillIsProjectile && _targetTile != null)
                    {
                        _raycast.UpdateProjectilePrediction(_currentUnit, out unit, _targetTile);
                        if (unit != null)
                            _targetTile = unit.CurrentTile;
                    }
                    _cursor.UpdateSkillCursor(_loadedSkill, _currentUnit.Skills.SelectedSkill.SkillData, _currentUnit, _targetTile);
                }
                else
                    _cursor.UpdateMoveCursor(_currentUnit, _currentTile, _targetTile);
                _currentTile = _targetTile;
            }

            if (unit != null && unit != _currentUnit)
                CombatUIController.Instance.TargetUnitRessourcesPanelController.SetTargetUnit(unit);
            else
                CombatUIController.Instance.TargetUnitRessourcesPanelController.ClearPannel();
        }

        public override void UnitTurn(CombatEntity unit)
        {
            base.UnitTurn(unit);
            CombatUIController.Instance.CurrentUnitRessourcesPanelController.SetTargetUnit(_currentUnit);
            UnlockInteraction();
        }

        public override void UnitActionEnd()
        {
            PlayerTurn = false;
            _canInteract = false;
            base.UnitActionEnd();
        }

        public void SkillLoaded()
        {
            //TODO Update spell req with unit status
            if (!_currentUnit.Ressources.CheckSkillRessourceRequirement(_currentUnit.Skills.SelectedSkill.SkillData.SkillDefinition))
            {
                Debug.Log("Missing ressources");
                _currentUnit.Skills.ClearSelectedSkill();
                return;
            }

            HashSet<Tile> tilesAffected = new HashSet<Tile>();
            List<Tile> tiles = new List<Tile>();
            List<Tile> tmpList = new List<Tile>();
            tiles.Add(_currentUnit.CurrentTile);

            for (int indexRange = 0; indexRange < _currentUnit.Skills.SelectedSkill.SkillStats.CastRange; indexRange++)
            {
                for (int currentTileIndex = 0; currentTileIndex < tiles.Count; currentTileIndex++)
                {
                    Tile tile = tiles[currentTileIndex];
                    for (int i = 0; i < 6; i++)
                    {
                        Tile tmp = tile.SearchData.Neighbors[i];

                        if (tmp != null)
                        {
                            if (indexRange >= _currentUnit.Skills.SelectedSkill.SkillStats.CastRange)
                                tmp.SetTileTmpState(TileTmpState.SkillRange);
                            tmpList.Add(tmp);
                        }
                    }
                }
                tiles.Clear();
                foreach (Tile t in tmpList)
                    if (tilesAffected.Add(t))
                        tiles.Add(t);
                tmpList.Clear();
            }
            _loadedSkill = Activator.CreateInstance(_currentUnit.Skills.SelectedSkill.SkillData.Skill) as CombatSkill;
            _loadedSkill.TilesAffected = tilesAffected.ToList();
            if (_currentUnit.Skills.SelectedSkill.SkillData.SkillDefinition.SkillTags.Contains(SkillTag.Projectile))
            {
                _shapeDrawer.SetShapeDrawState(true);
                _loadedSkillIsProjectile = true;
            }
            else
                _loadedSkillIsProjectile = false;
            _cursor.UpdateSkillCursor(_loadedSkill, _currentUnit.Skills.SelectedSkill.SkillData, _currentUnit, _currentUnit.CurrentTile);
        }

        public void SelectionClick(InputAction.CallbackContext context)
        {
            if (_currentTile == null)
                return;
            if (context.performed && PlayerTurn)
            {
                if (_currentUnit.Skills.SelectedSkill != null)
                {
                    //Tile targetTile = _raycast.GetTileFromCursor();
                    UpdateCursorPosition();
                    if (_targetTile != null && _cursor.AvailableTiles != null && _cursor.IsValid
                        && _loadedSkill.CheckRequirements(_currentUnit.Skills.SelectedSkill.SkillData.SkillDefinition, _currentUnit, _targetTile))
                    {
                        if (_targetTile != _currentUnit.CurrentTile)
                            _currentUnit.Movement.SetOrientation(_targetTile);

                        _loadedSkill.OnSkillLaunched(_currentUnit.Skills.SelectedSkill.SkillData.SkillDefinition, _currentUnit, _cursor, _targetTile);
                        if (_currentUnit.Skills.SelectedSkill.SkillData.SkillDefinition.SkillTags.Contains(SkillTag.Atk))
                            _currentUnit.Status.OnAtkLaunched(_targetTile);
                        else
                            _currentUnit.Status.OnSkillLaunched();
                        _shapeDrawer.SetShapeDrawState(false);
                        CombatUIController.Instance.CurrentUnitRessourcesPanelController.SetTargetUnit(_currentUnit);
                        CancelAction();
                        UpdateCursorPosition();
                    }
                }
                else if (_canInteract && _currentUnit != null && _currentTile && _currentTile.State == TileState.Empty)
                {
                    if (_currentUnit.Status.StatusSummary.IsRoot)
                    {
                        Debug.Log("Unit is root");
                        return;
                    }

                    string error = _currentUnit.Move(UnlockInteraction, _grid.FindPath(_currentUnit.CurrentTile, _currentTile));
                    if (error != null)
                    {
                        Debug.Log("Error: " + error);
                        return;
                    }
                    Debug.Log(_currentTile.Coordinates.ToString() + " " + _currentTile.State.ToString());
                }
            }
        }

        public void CancelAction()
        {
            if (_currentUnit.Skills.SelectedSkill != null)
            {
                if (_loadedSkill.TilesAffected != null)
                    foreach (Tile t in _loadedSkill.TilesAffected)
                        t.SetTileTmpState(TileTmpState.None);
                _currentUnit.Skills.ClearSelectedSkill();
            }
        }
    }
}