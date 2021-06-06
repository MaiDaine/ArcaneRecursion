using System.Collections.Generic;
using UnityEngine;

namespace ArcaneRecursion
{
    public class UnitRessources
    {
        public UnitStats UnitStats { get; private set; }
        public UnitStats UnitStatsMax { get; private set; }
        public List<ShieldCombatEffect> Shields { get; private set; }

        private readonly UnitController _unitController;
        private readonly UnitStatsModifier _statsModifier;

        #region Init
        public UnitRessources(UnitController controller)
        {
            _unitController = controller;
            _statsModifier.Reset();
            Shields = new List<ShieldCombatEffect>();
        }

        public void LoadStats(UnitStats stats)
        {
            UnitStats = new UnitStats(stats);
            UnitStatsMax = new UnitStats(stats);
        }
        #endregion /* Init */

        #region RessourceRequirement
        public bool CheckSkillRessourceRequirement(SkillDefinition skill)
        {
            SkillStats stats = _unitController.Status.SetSkillStatsFromCurrentState(skill.SkillStats, skill.SkillTags.Contains(SkillTag.Atk));
            return stats.APCost <= UnitStats.ActionPoints
                && stats.MPCost <= UnitStats.ActionPoints;
        }
        #endregion /* RessourceRequirement */

        #region AP
        public bool CanSpendAP(ref int amount)
        {
            amount = (amount * _statsModifier.ActionPoints.PercentValue / 100) + _statsModifier.ActionPoints.FlatValue;
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
            amount = (amount * _statsModifier.HealthPoints.PercentValue / 100) + _statsModifier.HealthPoints.FlatValue;
            return amount <= UnitStats.HealthPoints;
        }

        public bool OnHPLoss(int amount)
        {
            UnitStats.HealthPoints -= amount;
            if (UnitStats.HealthPoints <= 0)
            {
                _unitController.OnDeath();
                return true;
            }
            return false;
        }

        public bool OnKnockbackDamage()
        {
            return OnHPLoss(UnitStatsMax.HealthPoints / 10);
        }

        public void OnHPGain(int amount)
        {
            if (_unitController.Status.StatusSummary.IsCripple)
                amount /= 2;

            UnitStats.HealthPoints += amount;
            if (UnitStats.HealthPoints > UnitStatsMax.HealthPoints)
                UnitStatsMax.HealthPoints = UnitStats.HealthPoints;
        }
        #endregion /* HP */

        #region MP
        public bool CanSpendMP(ref int amount)
        {
            amount = (amount * _statsModifier.ManaPoints.PercentValue / 100) + _statsModifier.ManaPoints.FlatValue;
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

        #region ShieldEffect
        public void AddShieldEffect(ShieldCombatEffect effect) { Shields.Add(effect); }
        public void RemoveShieldEffect(ShieldCombatEffect effect) { Shields.Remove(effect); }
        #endregion /* ShieldEffect */

        #region DamageTaken
        public bool OnDamageTaken(int amount, DamageTypes damageType = DamageTypes.Magical)
        {
            if (damageType != DamageTypes.Arcane)
                amount = (100 - _unitController.CurrentStats.Defences[(int)damageType]) * amount / 100;

            while (Shields.Count > 0)
                if (Shields[Shields.Count - 1].OnDamageTaken(ref amount, damageType))
                    Shields.RemoveAt(Shields.Count - 1);

            if (amount > 0)
                return OnHPLoss(amount);

            return false;
        }

        public bool OnDamageTaken(ref int amount, DamageTypes damageType = DamageTypes.Magical)
        {
            if (damageType != DamageTypes.Arcane)
                amount = (100 - _unitController.CurrentStats.Defences[(int)damageType]) * amount / 100;

            while (Shields.Count > 0)
                if (Shields[Shields.Count - 1].OnDamageTaken(ref amount, damageType))
                    Shields.RemoveAt(Shields.Count - 1);

            if (amount > 0)
                return OnHPLoss(amount);

            return false;
        }
        #endregion /* DamageTaken */
    }
}