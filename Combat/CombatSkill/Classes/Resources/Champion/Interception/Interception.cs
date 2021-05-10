using System;

namespace ArcaneRecursion
{
    public class Interception : CombatSkill
    {
        private UnitController _caster;
        private UnitController _targetUnit;
        private Tile _targetTile;

        public override bool CheckRequirements(UnitController unit, CombatSkillObject data, Tile targetTile)
        {
            if (targetTile?.TileEntity.Team != 0)
            {
                _targetUnit = targetTile.TileEntity.GameObject.GetComponent<UnitController>();
                return true;
            }
            return false;
        }

        public override void OnSkillLaunched(UnitController caster, CombatSkillObject data, CombatCursor cursor, Tile targetTile)
        {
            base.OnSkillLaunched(caster, data, cursor, targetTile);

            Tile[] pathTiles = new Tile[cursor.PreviousTiles.Length - 2];
            for (int i = 1; i < cursor.PreviousTiles.Length - 1; i++)
                pathTiles[i - 1] = cursor.PreviousTiles[i];

            Action callback;
            if (cursor.PreviousTiles.Length > 3)
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
