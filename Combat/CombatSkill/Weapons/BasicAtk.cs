namespace ArcaneRecursion
{
    public class BasicAtk : DirectionalCombatSkill
    {
        public override void FrontAttack(UnitController caster, CombatSkillObject data, CombatCursor cursor, UnitController targetUnit)
        {
            targetUnit.Ressources.OnHPLoss(25, DamageTypes.Physical);//TODO SCALE AP
        }

        public override void SideAttack(UnitController caster, CombatSkillObject data, CombatCursor cursor, UnitController targetUnit)
        {
            targetUnit.Ressources.OnHPLoss(50, DamageTypes.Physical);//TODO SCALE AP
        }

        public override void BackAttack(UnitController caster, CombatSkillObject data, CombatCursor cursor, UnitController targetUnit)
        {
            targetUnit.Ressources.OnHPLoss(75, DamageTypes.Physical);//TODO SCALE AP
        }
    }
}
