namespace ArcaneRecursion
{
    public class UnitRessources
    {
        public UnitStats UnitStats { get; private set; }
        public UnitStats UnitStatsMax { get; private set; }

        private readonly UnitController _unitController;
        private readonly UnitStats _statModifierFlat;
        private readonly UnitStats _statModifierPercent;

        #region Init
        public UnitRessources(UnitController controller)
        {
            _unitController = controller;
            _statModifierFlat = new UnitStats();
            _statModifierPercent = UnitStats.DefaultPercentModifier();
        }

        public void LoadStats(UnitStats stats)
        {
            UnitStats = new UnitStats(stats);
            UnitStatsMax = new UnitStats(stats);
        }
        #endregion /* Init */

        public void ApplyFlatModifier(UnitStats modifier) { _statModifierFlat.ApplyModifier(modifier); }

        public void ApplyPercentModifier(UnitStats modifier) { _statModifierPercent.ApplyModifier(modifier); }

        public bool CheckSkillRessourceRequirement(SkillDefinition skill)
        {
            return skill.SkillStats.APCost <= UnitStats.ActionPoint
                && skill.SkillStats.MPCost <= UnitStats.ManaPoint;
        }

        #region AP
        public bool CanSpendAP(ref int amount)
        {
            amount = (amount * _statModifierPercent.ActionPoint / 100) + _statModifierFlat.ActionPoint;
            return amount <= UnitStats.ActionPoint;
        }

        public void OnAPLoss(int amount)
        {
            UnitStats.ActionPoint -= amount;
        }

        public void OnAPGain(int amount)
        {
            UnitStats.ActionPoint += amount;
            if (UnitStats.ActionPoint > UnitStatsMax.ActionPoint)
                UnitStatsMax.ActionPoint = UnitStats.ActionPoint;
        }
        #endregion /* AP */

        #region HP
        public bool CanSpendHP(ref int amount)
        {
            amount = (amount * _statModifierPercent.HealthPoint / 100) + _statModifierFlat.HealthPoint;
            return amount <= UnitStats.HealthPoint;
        }

        public bool OnHPLoss(int amount, DamageTypes damageType = DamageTypes.None)
        {
            UnitStats.HealthPoint -= amount;
            if (UnitStats.HealthPoint <= 0)
            {
                _unitController.OnDeath();
                return true;
            }
            return false;
        }

        public void OnHPGain(int amount)
        {
            UnitStats.HealthPoint += amount;
            if (UnitStats.HealthPoint > UnitStatsMax.HealthPoint)
                UnitStatsMax.HealthPoint = UnitStats.HealthPoint;
        }
        #endregion /* HP */

        #region MP
        public bool CanSpendMP(ref int amount)
        {
            amount = (amount * _statModifierPercent.ManaPoint / 100) + _statModifierFlat.ManaPoint;
            return amount <= UnitStats.ManaPoint;
        }

        public void OnMPLoss(int amount)
        {
            UnitStats.ManaPoint -= amount;
        }

        public void OnMPGain(int amount)
        {
            UnitStats.ManaPoint += amount;
            if (UnitStats.ManaPoint > UnitStatsMax.ManaPoint)
                UnitStatsMax.ManaPoint = UnitStats.ManaPoint;
        }
        #endregion /* MP */

    }
}