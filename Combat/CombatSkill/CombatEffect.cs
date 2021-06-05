using System.Collections.Generic;

namespace ArcaneRecursion
{
    public class CombatEffect
    {
        public const int PASSIVEFFECT = -1;

        public string Name;
        public int Duration;
        public int Potency;

        protected void SetName()
        {
            Name = SkillLibrary.GetNameFromType(this.GetType());
        }

        public virtual bool OnTurnStart(UnitController unit) { return false; }
        public virtual bool OnTurnEnd(UnitController unit) { return false; }
        public virtual void OnUnitDeath(UnitController unit) { }
        public virtual void OnDurationEnd(UnitController unit) { }
        public virtual bool OnDispell(UnitController unit) { return true; }
        public virtual bool OnSkillLaunched(UnitController unit) { return false; }
        public virtual bool OnAtkLaunched(UnitController unit, Tile targetTile) { return false; }
        public virtual void OnDirectionalAttackReceived(UnitController unitController, ref BasicOrientation from) { }
        public virtual bool OnEffectApply(UnitController unit, ref CombatEffect effect, List<SkillTag> skillTags) { return true; }
        public virtual bool OnUnitEnterTile(UnitController unit, Tile tile) { return false; }
        public virtual bool OnUnitExitTile(UnitController unit, Tile tile) { return false; }
    }
}