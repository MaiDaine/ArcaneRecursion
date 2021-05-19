using UnityEngine;
using System;

namespace ArcaneRecursion
{
    public class Intervention : CombatSkill
    {
        private UnitController _caster;
        private UnitController _targetUnit;
        private InterventionEffect _effect = null;

        public override bool CheckRequirements(SkillDefinition skillDefinition, UnitController unit, Tile targetTile)
        {
            if (targetTile?.TileEntity != null)
            {
                if (targetTile.TileEntity.Team == unit.CombatEntity.Team)
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
            if (_targetUnit)
            {
                _effect = new InterventionEffect(skillDefinition.SkillStats.Potency);
                callback = OnMoveEnd;
            }
            else
                callback = VoidCallback;

            _caster = caster;
            _targetTile = pathTiles[pathTiles.Length - 1];
            caster.Movement.MoveTo(callback, pathTiles);
        }

        public void OnMoveEnd()
        {
            _caster.Status.ApplyEffect(_effect);
            _targetUnit.Status.ApplyEffect(_effect);
        }
    }

    public class InterventionEffect : CombatEffect
    {
        private int _potency;

        public InterventionEffect(int potency)
        {
            base.SetName();
            Duration = 2;
            _potency = potency;
        }

        public void ApplyEnhancement(ref DefModifier defEnhancement)
        {
            defEnhancement.Res.PercentValue += _potency;
        }
    }
}