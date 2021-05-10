using UnityEngine;

namespace ArcaneRecursion
{
    public class DirectionalCombatSkill : CombatSkill
    {
        private BasicOrientation _onCastOrientation;
        private BasicOrientation _onReceivedOrientation;

        public virtual void FrontAttack(UnitController caster, CombatSkillObject data, CombatCursor cursor, UnitController targetUnit) { }
        public virtual void SideAttack(UnitController caster, CombatSkillObject data, CombatCursor cursor, UnitController targetUnit) { }
        public virtual void BackAttack(UnitController caster, CombatSkillObject data, CombatCursor cursor, UnitController targetUnit) { }

        public override void OnSkillLaunched(UnitController caster, CombatSkillObject data, CombatCursor cursor, Tile targetTile)
        {
            base.OnSkillLaunched(caster, data, cursor, targetTile);

            UnitController targetUnit = targetTile.TileEntity.GameObject.GetComponent<UnitController>();
            _onCastOrientation = HexCoordinates.GetOrientation(caster.Movement.Orientation, targetUnit.Movement.Orientation);
            _onReceivedOrientation = targetUnit.Status.OnDirectionalAttackReceived(_onCastOrientation);
            // TODO ??
            switch (_onCastOrientation)
            {
                case BasicOrientation.Front:
                    FrontAttack(caster, data, cursor, targetUnit);
                    break;
                case BasicOrientation.FrontSide:
                case BasicOrientation.BackSide:
                    SideAttack(caster, data, cursor, targetUnit);
                    break;
                case BasicOrientation.Back:
                    BackAttack(caster, data, cursor, targetUnit);
                    break;
            }
        }
    }
}