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

        public virtual void OnDurationEnd(UnitController unit) { }
        public virtual bool OnDispell(UnitController unit) { return true; }
        public virtual bool OnTurnEnd(UnitController unit) { return false; }
        public virtual bool OnSkillLaunched(UnitController unit) { return false; }
        public virtual bool OnAtkLaunched(UnitController unit) { return false; }
        public virtual void OnDirectionalAttackReceived(UnitController unitController, ref BasicOrientation from) { }

        /*
            Move
            Atk
            OnAtk
            OnDmg
            OnDmg TYPE 
            OnHeal
            OnShield
        */
    }
}