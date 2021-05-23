using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArcaneRecursion
{
    public struct SkillModifier
    {
        public StatusModifierVariable AP;
        public StatusModifierVariable MP;
        public StatusModifierVariable Potency;

        public void Reset()
        {
            AP.FlatValue = 0;
            AP.PercentValue = 100;
            MP.FlatValue = 0;
            MP.PercentValue = 100;
            Potency.FlatValue = 0;
            Potency.PercentValue = 100;
        }
    }

    public interface ISkillEnhancement
    {
        void ApplyEnhancement(ref SkillModifier castEnhancement);
    }

    public interface IAtkEnhancement
    {
        void ApplyEnhancement(ref SkillModifier atkEnhancement);
    }
}
