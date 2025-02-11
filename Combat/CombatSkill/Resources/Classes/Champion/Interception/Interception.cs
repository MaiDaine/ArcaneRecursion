﻿using UnityEngine;
using System;

namespace ArcaneRecursion
{
    public class Interception : CombatSkill
    {
        private UnitController _caster;
        private UnitController _targetUnit;
        private Tile _targetTile;

        public override bool CheckRequirements(SkillDefinition skillDefinition, UnitController unit, Tile targetTile)
        {
            if (!unit.Status.StatusSummary.IsRoot && (targetTile?.TileEntity?.Team ?? 0) != 0)
            {
                _targetUnit = targetTile.TileEntity.GameObject.GetComponent<UnitController>();
                return true;
            }
            return false;
        }

        public override void OnSkillLaunched(SkillDefinition skillDefinition, UnitController caster, CombatCursor cursor, Tile targetTile)
        {
            base.OnSkillLaunched(skillDefinition, caster, cursor, targetTile);

            Tile[] pathTiles = new Tile[cursor.AvailableTiles.Length - 2];
            for (int i = 1; i < cursor.AvailableTiles.Length - 1; i++)
                pathTiles[i - 1] = cursor.AvailableTiles[i];

            Action callback;
            if (cursor.AvailableTiles.Length > 3)
                callback = OnMoveEnd;
            else
                callback = VoidCallback;

            _caster = caster;
            _targetTile = pathTiles[pathTiles.Length - 1];
            caster.Movement.MoveTo(callback, pathTiles);
        }

        public void OnMoveEnd()
        {
            if (_caster.CurrentTile == _targetTile)
                CombatTurnController.Instance.ChangeUnitOrder(_targetUnit.CombatEntity, 25);
        }
    }
}
