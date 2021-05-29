using System.Collections.Generic;
using System;
using UnityEngine;

namespace ArcaneRecursion
{
    public class UnitStatus
    {
        public List<CombatEffect> ActiveEffects { get; private set; }
        public DefModifier DefModifier;
        public UnitStatusSummary StatusSummary;

        private readonly UnitController _unitController;
        private SkillModifier _skillModifier;
        private SkillModifier _atkModifier;
        private List<CombatEffect> _pendingEffect;
        private bool _addToPendingEffects = false;

        #region Init
        public UnitStatus(UnitController controller)
        {
            ActiveEffects = new List<CombatEffect>();
            StatusSummary = new UnitStatusSummary { IsRoot = false };
            _unitController = controller;
            _pendingEffect = new List<CombatEffect>();
        }
        #endregion /* Init */

        public void ApplyEffect(CombatEffect effect)
        {
            if (_addToPendingEffects)
            {
                _pendingEffect.Add(effect);
                return;
            }

            List<SkillTag> skillTags = SkillLibrary.ClassEffectsDatas[effect.Name].EffectDefinition.SkillTags;
            foreach (CombatEffect e in ActiveEffects)
            {
                if (!e.OnEffectApply(_unitController, ref effect, skillTags))
                    return;
            }

            ActiveEffects.Add(effect);
            if (effect is ShieldCombatEffect)
                _unitController.Ressources.AddShieldEffect(effect as ShieldCombatEffect);
            if (effect is ISkillEnhancement || effect is IDefEnhancement)
                RefreshEnhancement();
        }

        public SkillStats SetSkillStatsFromCurrentState(SkillStats baseStats, bool isAtk = false)
        {
            SkillStats result = baseStats;
            SkillModifier modifier = isAtk ? _atkModifier : _skillModifier;

            result.APCost = baseStats.APCost - modifier.AP.FlatValue;
            result.APCost = result.APCost * modifier.AP.PercentValue / 100;
            result.MPCost = baseStats.MPCost - modifier.MP.FlatValue;
            result.MPCost = result.MPCost * modifier.MP.PercentValue / 100;
            result.Potency = baseStats.Potency - modifier.Potency.FlatValue;
            result.Potency = result.Potency * modifier.Potency.PercentValue / 100;

            return result;
        }

        #region UnitTurn Cycle
        public void OnStartTurn()
        {
            _addToPendingEffects = true;
            foreach (CombatEffect effect in ActiveEffects)
                if (effect.OnTurnStart(_unitController))
                    effect.Duration = 0;

            ActiveEffects.RemoveAll(e => e.Duration == 0);
            BatchApplyEffect();
        }

        public void OnEndTurn()
        {
            _addToPendingEffects = true;
            foreach (CombatEffect effect in ActiveEffects)
            {
                if (effect.OnTurnEnd(_unitController))
                    effect.Duration = 0;
                if (effect.Duration > 0)
                {
                    effect.Duration--;
                    if (effect.Duration == 0)
                    {
                        effect.OnDurationEnd(_unitController);
                        if (effect is ShieldCombatEffect)
                            _unitController.Ressources.RemoveShieldEffect(effect as ShieldCombatEffect);
                    }
                }
            }
            ActiveEffects.RemoveAll(e => e.Duration == 0);
            BatchApplyEffect();
        }
        #endregion /* UnitTurn Cycle */

        #region OnEffects
        public void OnSkillLaunched()
        {
            _addToPendingEffects = true;
            foreach (CombatEffect effect in ActiveEffects)
                if (effect.OnSkillLaunched(_unitController))
                    effect.Duration = 0;

            ActiveEffects.RemoveAll(e => e.Duration == 0);
            BatchApplyEffect();
        }

        public void OnAtkLaunched()
        {
            _addToPendingEffects = true;
            foreach (CombatEffect effect in ActiveEffects)
                if (effect.OnAtkLaunched(_unitController))
                    effect.Duration = 0;

            ActiveEffects.RemoveAll(e => e.Duration == -0);
            BatchApplyEffect();
        }

        public BasicOrientation OnDirectionalAttackReceived(BasicOrientation from)
        {
            foreach (CombatEffect effect in ActiveEffects)
                effect.OnDirectionalAttackReceived(_unitController, ref from);

            return from;
        }

        public void OnDispell()
        {
            _addToPendingEffects = true;
            foreach (CombatEffect effect in ActiveEffects)
                if (effect.OnDispell(_unitController))
                    effect.Duration = 0;

            ActiveEffects.RemoveAll(e => e.Duration == 0);
            BatchApplyEffect();
        }
        #endregion /* OnEffects */

        private void RefreshEnhancement()//TODO APPLY AS UNARY ? //TODO CONTROL REFRESH
        {
            DefModifier.Reset();
            _skillModifier.Reset();
            _atkModifier.Reset();

            foreach (CombatEffect e in ActiveEffects)
            {
                ISkillEnhancement skillEnhancement = e as ISkillEnhancement;
                skillEnhancement?.ApplyEnhancement(ref _skillModifier);

                IAtkEnhancement atkEnhancement = e as IAtkEnhancement;
                atkEnhancement?.ApplyEnhancement(ref _atkModifier);

                IDefEnhancement defEnhancement = e as IDefEnhancement;
                defEnhancement?.ApplyEnhancement(ref DefModifier);
            }

            StatusModifierVariable modifier;
            for (int i = 0; i < _unitController.CurrentStats.Defences.Length; i++)
            {
                modifier = DefModifier.GetModifier((DamageTypes)i);
                _unitController.CurrentStats.Defences[i] = modifier.FlatValue + (modifier.PercentValue / 100);
            }
        }

        private void BatchApplyEffect()
        {
            _addToPendingEffects = false;
            for (int i = 0; i < _pendingEffect.Count; i++)
            {
                CombatEffect effect = _pendingEffect[i];
                List<SkillTag> skillTags = SkillLibrary.ClassEffectsDatas[effect.Name].EffectDefinition.SkillTags;
                foreach (CombatEffect e in ActiveEffects)
                    if (!e.OnEffectApply(_unitController, ref effect, skillTags))
                        return;

                ActiveEffects.Add(effect);
                if (effect is ShieldCombatEffect)
                    _unitController.Ressources.AddShieldEffect(effect as ShieldCombatEffect);
            }
            RefreshEnhancement();
        }
    }
}