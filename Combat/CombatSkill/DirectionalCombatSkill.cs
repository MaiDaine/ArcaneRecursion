namespace ArcaneRecursion
{
    public class DirectionalCombatSkill : CombatSkill
    {
        private BasicOrientation _onCastOrientation;
        private BasicOrientation _onReceivedOrientation;

        public virtual void FrontAttack(SkillDefinition skillDefinition, UnitController caster, CombatCursor cursor, UnitController targetUnit) { }
        public virtual void SideAttack(SkillDefinition skillDefinition, UnitController caster, CombatCursor cursor, UnitController targetUnit) { }
        public virtual void BackAttack(SkillDefinition skillDefinition, UnitController caster, CombatCursor cursor, UnitController targetUnit) { }

        public override void OnSkillLaunched(SkillDefinition skillDefinition, UnitController caster, CombatCursor cursor, Tile targetTile)
        {
            base.OnSkillLaunched(skillDefinition, caster, cursor, targetTile);
            UnitController targetUnit = targetTile.TileEntity.GameObject.GetComponent<UnitController>();
            _onCastOrientation = HexCoordinates.GetOrientation(caster.Movement.Orientation, targetUnit.Movement.Orientation);
            _onReceivedOrientation = targetUnit.Status.OnDirectionalAttackReceived(_onCastOrientation);

            switch (_onCastOrientation)
            {
                case BasicOrientation.Front:
                    FrontAttack(skillDefinition, caster, cursor, targetUnit);
                    break;
                case BasicOrientation.FrontSide:
                case BasicOrientation.BackSide:
                    SideAttack(skillDefinition, caster, cursor, targetUnit);
                    break;
                case BasicOrientation.Back:
                    BackAttack(skillDefinition, caster, cursor, targetUnit);
                    break;
            }
        }
    }
}