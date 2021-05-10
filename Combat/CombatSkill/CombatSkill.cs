using System.Collections.Generic;

namespace ArcaneRecursion
{
    public enum CombatSkillReference
    {
        Null, BasicAtk,
        ArcaneBolt,
        ArcaneFlow,
        Interception,
        Overgrowth
    }

    public abstract class CombatSkill
    {
        public List<Tile> TilesAffected { get; set; }

        public virtual bool CheckRequirements(UnitController unit, CombatSkillObject data, Tile targetTile)
        {
            return true;
        }

        public virtual void OnSkillLaunched(UnitController caster, CombatSkillObject data, CombatCursor cursor, Tile targetTile)
        {
            int APCost = 0;
            int MPCost = 0;

            caster.Status.GetRealSkillCost(data, ref APCost, ref MPCost);
            caster.Ressources.OnAPLoss(APCost);
            caster.Ressources.OnMPLoss(MPCost);
            caster.Skills.OnSkillLaunched();
        }

        public void VoidCallback() { }
    }
}