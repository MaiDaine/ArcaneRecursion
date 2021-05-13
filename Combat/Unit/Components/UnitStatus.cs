﻿using System.Collections.Generic;

namespace ArcaneRecursion
{
    public class UnitStatus
    {
        public List<CombatEffect> ActiveEffects { get; private set; }

        private readonly UnitController _unitController;
        private SkillModifier _skillModifier;

        #region Init
        public UnitStatus(UnitController controller)
        {
            _unitController = controller;
            ActiveEffects = new List<CombatEffect>();
        }
        #endregion /* Init */

        public void ApplyEffect(CombatEffect effect)
        {
            ActiveEffects.Add(effect);
            if (effect is ISkillEnhancement)
                RefreshEnhancement();
        }

        public SkillStats SetSkillStatsFromCurrentState(SkillStats baseStats)
        {
            SkillStats result = baseStats;
            result.APCost = baseStats.APCost - _skillModifier.APFlat;
            result.APCost = (int)(result.APCost * _skillModifier.APPercent);
            result.MPCost = baseStats.MPCost - _skillModifier.MPFlat;
            result.MPCost = (int)(result.MPCost * _skillModifier.MPPercent);
            //TODO Potency scale
            return result;
        }

        #region UnitTurn Cycle
        public void OnStartTurn()
        {
            RefreshEnhancement();
        }

        public void OnEndTurn()
        {
            int currentSize = ActiveEffects.Count;
            for (int i = 0; i < currentSize; i++)
            {
                if (ActiveEffects[i].Duration > 0)
                {
                    ActiveEffects[i].Duration--;
                    if (ActiveEffects[i].Duration == 0)
                        ActiveEffects[i].OnDurationEnd(_unitController);
                }
            }
            ActiveEffects.RemoveAll(e => e.Duration == 0);
        }
        #endregion /* UnitTurn Cycle */

        #region OnEffects
        public void OnSkillLaunched()
        {
            int currentSize = ActiveEffects.Count;
            for (int i = 0; i < currentSize; i++)
            {
                if (ActiveEffects[i].OnSkillLaunched(_unitController))
                    ActiveEffects[i].Duration = -2;
            }
            ActiveEffects.RemoveAll(e => e.Duration == -2);
            RefreshEnhancement();
        }

        public BasicOrientation OnDirectionalAttackReceived(BasicOrientation from)
        {
            int currentSize = ActiveEffects.Count;
            for (int i = 0; i < currentSize; i++)
                ActiveEffects[i].OnDirectionalAttackReceived(_unitController, ref from);
            return from;
        }
        #endregion /* OnEffects */

        private void RefreshEnhancement()//TODO APPLY AS UNARY ? //TODO CONTROL REFRESH
        {
            _skillModifier.APFlat = 0;
            _skillModifier.APPercent = 1;
            _skillModifier.MPFlat = 0;
            _skillModifier.MPPercent = 1;

            foreach (CombatEffect e in ActiveEffects)
            {
                ISkillEnhancement tmp = e as ISkillEnhancement;
                tmp?.ApplyEnhancement(ref _skillModifier);
            }
        }
    }
}