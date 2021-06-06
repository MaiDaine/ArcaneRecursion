using UnityEngine;

namespace ArcaneRecursion
{
    public class FluxControl : CombatSkill
    {
        public override void OnSkillLaunched(SkillDefinition skillDefinition, UnitController caster, CombatCursor cursor, Tile targetTile)
        {
            caster.Status.ApplyEffect(new FluxControlEffectPassive(caster, skillDefinition.SkillStats.Potency));
        }
    }

    public class FluxControlEffectPassive : CombatEffect
    {
        private Tile[] _affectedTiles;
        private bool _unitTurn = false;

        public FluxControlEffectPassive(UnitController caster, int potency)
        {
            base.SetName();
            Duration = PASSIVEFFECT;
            _affectedTiles = caster.CurrentTile.SearchData.Neighbors;
            ApplyEffect();
            Potency = potency;
        }

        public override bool OnDispell(UnitController unit)
        {
            return false;
        }

        public override bool OnTurnStart(UnitController unit)
        {
            _unitTurn = true;
            RemoveEffect();
            _affectedTiles = null;
            return false;
        }

        public override bool OnTurnEnd(UnitController unit)
        {
            _unitTurn = false;
            _affectedTiles = unit.CurrentTile.SearchData.Neighbors;
            ApplyEffect();
            return false;
        }

        public override void OnUnitDeath(UnitController unit)
        {
            RemoveEffect();
            _affectedTiles = null;
        }

        public override bool OnUnitEnterTile(UnitController unit, Tile targetTile)
        {
            if (_unitTurn)
                return false;

            RemoveEffect();
            _affectedTiles = targetTile.SearchData.Neighbors;
            ApplyEffect();
            return false;
        }

        private void ApplyEffect()
        {
            foreach (Tile tile in _affectedTiles)
                if (tile != null)
                    tile.MoveCostPercent += Potency;
        }

        private void RemoveEffect()
        {
            foreach (Tile tile in _affectedTiles)
                if (tile != null)
                    tile.MoveCostPercent -= Potency;
        }
    }
}