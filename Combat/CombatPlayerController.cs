using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace ArcaneRecursion
{
    public class CombatPlayerController : CombatController
    {
        public bool PlayerTurn { get; set; }

        [SerializeField] private RaycastHelper _raycast;

        private Tile _currentTile = null;
        private bool _canInteract = false;
        private CombatSkill _loadedSkill = null;

        private void LateUpdate()
        {
            if (_canInteract)
                UpdateCursorPosition();
        }

        private void UnlockInteraction() { _canInteract = true; }

        private void UpdateCursorPosition()
        {
            _raycast.GetEntityFromCursor(out UnitController unit, out Tile tile);
            if (tile != _currentTile)
            {
                if (_currentUnit.Skills.SelectedSkill != null)
                    _cursor.UpdateSkillCursor(_loadedSkill, _currentUnit.Skills.SelectedSkill.CombatSkillObject, _currentUnit, tile);
                else
                    _cursor.UpdateMoveCursor(_currentUnit, _currentTile, tile);
                _currentTile = tile;
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
            //TODO UPDATE SPELL REQ WITH UNIT STATUS
            if (!_currentUnit.Ressources.CheckSkillRessourceRequirement(_currentUnit.Skills.SelectedSkill))
            {
                Debug.Log("MISSING RESSOURCES TO CAST " + _currentUnit.Skills.SelectedSkill.CombatSkillObject.NodeDefinition.Name);
                return;
            }

            List<Tile> tiles = new List<Tile>();
            tiles.Add(_currentUnit.CurrentTile);

            for (int indexRange = 0; indexRange < _currentUnit.Skills.SelectedSkill.CombatSkillObject.CastRange; indexRange++)
            {
                List<Tile> tmpList = new List<Tile>();
                for (int currentTileIndex = 0; currentTileIndex < tiles.Count; currentTileIndex++)
                {
                    Tile tile = tiles[currentTileIndex];
                    for (int i = 0; i < 6; i++)
                    {
                        Tile tmp = tile.SearchData.Neighbors[i];

                        if (tmp != null)
                        {
                            tmp.SetTileTmpState(TileTmpState.SkillRange);
                            tmpList.Add(tmp);
                        }
                    }
                }
                tiles.Clear();
                tiles.AddRange(tmpList);
            }
            _loadedSkill = Activator.CreateInstance(Type.GetType("ArcaneRecursion." + _currentUnit.Skills.SelectedSkill.CombatSkillObject.NodeDefinition.Name)) as CombatSkill;
            _loadedSkill.TilesAffected = tiles;
        }

        public void SelectionClick(InputAction.CallbackContext context)
        {
            //TODO remplace condition
            if (_currentTile == null) //TODO RAYCAST PLAYER
                return;
            //TODO remplace condition
            if (context.performed && PlayerTurn)
            {
                if (_currentUnit.Skills.SelectedSkill != null)
                {
                    Tile targetTile = _raycast.GetTileFromCursor();
                    if (targetTile != null && _loadedSkill.CheckRequirements(_currentUnit, _currentUnit.Skills.SelectedSkill.CombatSkillObject, targetTile))
                    {
                        if (targetTile != _currentUnit.CurrentTile)
                            _currentUnit.Movement.SetOrientation(targetTile);

                        _loadedSkill.OnSkillLaunched(_currentUnit, _currentUnit.Skills.SelectedSkill.CombatSkillObject, _cursor, targetTile);
                        _currentUnit.Status.OnSkillLaunched();
                        CombatUIController.Instance.CurrentUnitRessourcesPanelController.SetTargetUnit(_currentUnit);
                        CancelAction();
                        UpdateCursorPosition();
                        CombatUIController.Instance.CurrentUnitRessourcesPanelController.SetTargetUnit(_currentUnit);
                    }
                }
                else if (_canInteract && _currentUnit != null && _currentTile && _currentTile.State == TileState.Empty)//TODO
                {
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