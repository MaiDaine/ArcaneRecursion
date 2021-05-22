using System.Collections.Generic;

namespace ArcaneRecursion
{
    public class UnitStatus
    {
        public List<CombatEffect> ActiveEffects { get; private set; }
        public DefModifier DefModifier;

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
            if (effect is ShieldCombatEffect)
                _unitController.Ressources.AddShieldEffect(effect as ShieldCombatEffect);
            if (effect is ISkillEnhancement || effect is IDefEnhancement)
                RefreshEnhancement();
        }

        public SkillStats SetSkillStatsFromCurrentState(SkillStats baseStats)
        {
            SkillStats result = baseStats;
            result.APCost = baseStats.APCost - _skillModifier.AP.FlatValue;
            result.APCost = result.APCost * _skillModifier.AP.PercentValue / 100;
            result.MPCost = baseStats.MPCost - _skillModifier.MP.FlatValue;
            result.MPCost = result.MPCost * _skillModifier.MP.PercentValue / 100;
            //TODO Potency scale
            return result;
        }

        #region UnitTurn Cycle
        public void OnStartTurn()
        {
            int currentSize = ActiveEffects.Count;
            for (int i = 0; i < currentSize; i++)
            {
                if (ActiveEffects[i].Duration > 0)
                {
                    ActiveEffects[i].Duration--;
                    if (ActiveEffects[i].Duration == 0)
                    {
                        ActiveEffects[i].OnDurationEnd(_unitController);
                        if (ActiveEffects[i] is ShieldCombatEffect)
                            _unitController.Ressources.RemoveShieldEffect(ActiveEffects[i] as ShieldCombatEffect);
                    }
                }
            }
            ActiveEffects.RemoveAll(e => e.Duration == 0);

            RefreshEnhancement();
        }

        public void OnEndTurn() { }
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
            DefModifier.Reset();
            _skillModifier.Reset();

            foreach (CombatEffect e in ActiveEffects)
            {
                ISkillEnhancement skillEnhancement = e as ISkillEnhancement;
                skillEnhancement?.ApplyEnhancement(ref _skillModifier);

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
    }
}