using UnityEngine;

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
            return skill.SkillStats.APCost <= UnitStats.ActionPoints
                && skill.SkillStats.MPCost <= UnitStats.ActionPoints;
        }

        public bool CheckSkillRessourceRequirement(SkillDefinition skill, SkillStats addedCost)
        {
            return skill.SkillStats.APCost + addedCost.APCost <= UnitStats.ActionPoints
                && skill.SkillStats.MPCost + addedCost.MPCost <= UnitStats.ManaPoints;
        }

        #region AP
        public bool CanSpendAP(ref int amount)
        {
            amount = (amount * _statModifierPercent.ActionPoints / 100) + _statModifierFlat.ActionPoints;
            return amount <= UnitStats.ActionPoints;
        }

        public void OnAPLoss(int amount)
        {
            UnitStats.ActionPoints -= amount;
        }

        public void OnAPGain(int amount)
        {
            UnitStats.ActionPoints += amount;
            if (UnitStats.ActionPoints > UnitStatsMax.ActionPoints)
                UnitStatsMax.ActionPoints = UnitStats.ActionPoints;
        }
        #endregion /* AP */

        #region HP
        public bool CanSpendHP(ref int amount)
        {
            amount = (amount * _statModifierPercent.HealthPoints / 100) + _statModifierFlat.HealthPoints;
            return amount <= UnitStats.HealthPoints;
        }

        public bool OnHPLoss(int amount, DamageTypes damageType = DamageTypes.None)
        {
            UnitStats.HealthPoints -= amount;
            if (UnitStats.HealthPoints <= 0)
            {
                _unitController.OnDeath();
                return true;
            }
            return false;
        }

        public void OnHPGain(int amount)
        {
            UnitStats.HealthPoints += amount;
            if (UnitStats.HealthPoints > UnitStatsMax.HealthPoints)
                UnitStatsMax.HealthPoints = UnitStats.HealthPoints;
        }
        #endregion /* HP */

        #region MP
        public bool CanSpendMP(ref int amount)
        {
            amount = (amount * _statModifierPercent.ManaPoints / 100) + _statModifierFlat.ManaPoints;
            return amount <= UnitStats.ManaPoints;
        }

        public void OnMPLoss(int amount)
        {
            UnitStats.ManaPoints -= amount;
        }

        public void OnMPGain(int amount)
        {
            UnitStats.ManaPoints += amount;
            if (UnitStats.ManaPoints > UnitStatsMax.ManaPoints)
                UnitStatsMax.ManaPoints = UnitStats.ManaPoints;
        }
        #endregion /* MP */

    }
}