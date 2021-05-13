using System.Collections.Generic;

namespace ArcaneRecursion
{
    public abstract class CombatSkill
    {
        public List<Tile> TilesAffected { get; set; }
        protected SkillStats _updatedStats;

        public virtual bool CheckRequirements(SkillDefinition skillDefinition, UnitController unit, Tile targetTile)
        {
            return true;
        }

        public virtual void OnSkillLaunched(SkillDefinition skillDefinition, UnitController caster, CombatCursor cursor, Tile targetTile)
        {
            _updatedStats = caster.Status.SetSkillStatsFromCurrentState(skillDefinition.SkillStats);
            caster.Ressources.OnAPLoss(_updatedStats.APCost);
            caster.Ressources.OnMPLoss(_updatedStats.MPCost);
            caster.Skills.OnSkillLaunched();
        }

        public void VoidCallback() { }
    }
}